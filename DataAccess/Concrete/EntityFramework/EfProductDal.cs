using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Entities.Concrete.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null ? GetResult(context).ToList() : GetResult(context).Where(filter).ToList();
            }
        }

        public ProductDetailDto GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {

                return GetResult(context).SingleOrDefault(filter);
            }
        }

        private IQueryable<ProductDetailDto> GetResult(NorthwindContext context)
        {
            var result = from p in context.Products
                         join c in context.Categories
                         on p.CategoryId equals c.CategoryId
                         select new ProductDetailDto()
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             UnitsInStock = p.UnitsInStock,
                             CategoryName = c.CategoryName
                         };
            return result;
        }
    }
}