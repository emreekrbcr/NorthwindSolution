using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<List<Product>>> GetAll();
        Task<IDataResult<Product>> GetById(int id);
        Task<IDataResult<List<Product>>> GetAllByCategoryId(int id);
        Task<IDataResult<List<Product>>> GetAllByUnitPrice(decimal min, decimal max);
        Task<IDataResult<int>> Count(Expression<Func<Product, bool>> filter = null);
        Task<IDataResult<bool>> Any(Expression<Func<Product, bool>> filter);

        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
        Task<IResult> Delete(Product product);

        Task<IResult> AddTransactionalTest(Product product1, Product product2);
        
        Task<IDataResult<List<ProductDetailDto>>> GetProductDetails();
        Task<IDataResult<ProductDetailDto>> GetProductDetail(int id);

        /// <summary>
        /// Returns product details that lower than stockLimit
        /// </summary>
        /// <param name="stockLimit"></param>
        /// <returns></returns>
        Task<IDataResult<List<ProductDetailDto>>> GetProductDetailsByStock(short stockLimit);
    }
}
