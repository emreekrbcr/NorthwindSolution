using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null);
        ProductDetailDto GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter);
    }
}
