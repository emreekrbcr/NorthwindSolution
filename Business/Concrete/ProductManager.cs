using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Utilities.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        //Bir EntityManager kendisi dışında hiçbir EntityDal'ı enjekte edemez. Bundan dolayı dışardan başka bir Entity'nin metodlarına erişilmek isteniyorsa onun Service'ini enjekte edebiliriz!!!

        //Sebebi her entity'nin kendi içerisinde iş kuralları ve authorization gibi veriye erişmek için yetkisinin olması gerektiği durumlar olabilir. Ayrıca örneğin kategori sayısı 15'i geçtiyse ürün eklenemez gibi bir kuralımız vardı. Yarın öbür gün bu kurala ek olarak deniz mahsülleri ve içecekler bu kuralın dışında kalsın gibi bir kural veya ilave kurallar getirmek istersek ve bunları da ProductManager'ın içerisinde farklı farklı yerlerde kullanıyorsak ilerde hepsini değiştirmemiz gerekeceği için sıkıntı yaşarız. Günümüzde mikroservis mimarisinde bu yazdığımız her IBilmemneService için bir API oluşturup farklı birer servis olarak yayına alınır.     

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal; //ctor injection
            _categoryService = categoryService;
        }

        #region Reading
        [Cache]
        //[Performance(3)]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 19)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.SystemMessages.MaintenanceTime);
            //}

            //Thread.Sleep(6000);

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),
                Messages.ProductMessages.SuccessMessages.ProductsListed);
        }

        [Cache]
        //[Performance(3)]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id),
                Messages.ProductMessages.SuccessMessages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),
                Messages.ProductMessages.SuccessMessages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(
                _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max),
                Messages.ProductMessages.SuccessMessages.ProductsListed);
        }

        public IDataResult<int> Count(Expression<Func<Product, bool>> filter = null)
        {
            return new SuccessDataResult<int>(_productDal.Count(filter), Messages.CommonMessages.OperationSucceeded);
        }

        public IDataResult<bool> Any(Expression<Func<Product, bool>> filter)
        {
            return new SuccessDataResult<bool>(_productDal.Any(filter), Messages.CommonMessages.OperationSucceeded);
        }

        #endregion

        #region Writing
        [SecuredOperation("product.add,admin")]
        [Validation(typeof(ProductValidator))]
        [CacheRemove("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryLimitExceeded(product.CategoryId),
                CheckIfProductNameAlreadyExists(product.ProductName), CheckIfTotalCategoryCountExceeded());

            if (!result.Success)
            {
                return new ErrorResult(result.Messages);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductMessages.SuccessMessages.ProductAdded);
        }

        [CacheRemove("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductMessages.SuccessMessages.ProductUpdated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductMessages.SuccessMessages.ProductDeleted);
        }

        [Transaction]
        public IResult AddTransactionalTest(Product product1, Product product2)
        {
            _productDal.Add(product1);

            if (product2.UnitPrice < 20)
            {
                throw new Exception("Ürün2'nin birim fiyatı 20'den küçük olduğu için eklenemiyor");
            }

            _productDal.Add(product2);
            return new SuccessResult(Messages.CommonMessages.OperationSucceeded);
        }

        #endregion

        #region DtoReading

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),
                Messages.ProductMessages.SuccessMessages.ProductDetailsListed);
        }

        public IDataResult<ProductDetailDto> GetProductDetail(int id)
        {
            return new SuccessDataResult<ProductDetailDto>(_productDal.GetProductDetail(p => p.ProductId == id),
                Messages.ProductMessages.SuccessMessages.ProductDetailListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailsByStock(short stockLimit)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(
                _productDal.GetProductDetails(p => p.UnitsInStock <= stockLimit),
                Messages.ProductMessages.SuccessMessages.ProductDetailsListed);
        }

        #endregion

        #region BusinessRules

        private IResult CheckIfCategoryLimitExceeded(int categoryId)
        {
            //int result = _productDal.GetAll(p => p.CategoryId == categoryId).Count; //koleksiyona sorgu atar
            int result = _productDal.Count(p => p.CategoryId == categoryId);
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductMessages.ErrorMessages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameAlreadyExists(string productName)
        {
            //bool result = _productDal.GetAll(p => p.ProductName == productName).Any(); //koleksiyona sorgu atar
            bool result = _productDal.Any(p => p.ProductName == productName);
            if (result)
            {
                return new ErrorResult(Messages.ProductMessages.ErrorMessages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        //Bu kural ProductManager'ın Category servisini nasıl yorumladığıyla alakalı ondan dolayı buraya yazarız. Eğer kendi içerisinde çalışması gereken bir metod olsaydı direk ordan çağırır alırdık.
        private IResult CheckIfTotalCategoryCountExceeded()
        {
            //int count = _categoryService.GetAll().Data.Count; //koleksiyona sorgu atar
            int count = _categoryService.Count().Data;
            if (count > 15)
            {
                return new ErrorResult(Messages.ProductMessages.ErrorMessages.TotalCategoryCountExceeded);
            }
            return new SuccessResult();
        }

        #endregion
    }
}
