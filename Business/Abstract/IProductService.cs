using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);
        IDataResult<int> Count(Expression<Func<Product, bool>> filter = null);
        IDataResult<bool> Any(Expression<Func<Product, bool>> filter);

        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);

        IResult AddTransactionalTest(Product product1, Product product2);
        
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<ProductDetailDto> GetProductDetail(int id);

        /// <summary>
        /// Returns product details that lower than stockLimit
        /// </summary>
        /// <param name="stockLimit"></param>
        /// <returns></returns>
        IDataResult<List<ProductDetailDto>> GetProductDetailsByStock(short stockLimit);
    }
}
