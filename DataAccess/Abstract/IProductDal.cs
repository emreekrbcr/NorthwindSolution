using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<List<ProductDetailDto>> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null);
        Task<ProductDetailDto> GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter);
    }
}