using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public async Task<List<ProductDetailDto>> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null ? 
                    await GetResult(context).ToListAsync() : 
                    await GetResult(context).Where(filter).ToListAsync();
            }
        }

        public async Task<ProductDetailDto> GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {

                return await GetResult(context).SingleOrDefaultAsync(filter);
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