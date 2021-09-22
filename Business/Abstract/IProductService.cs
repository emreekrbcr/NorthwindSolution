using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);

        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
        
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
