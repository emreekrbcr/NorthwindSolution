using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<List<Category>>> GetAll();
        Task<IDataResult<int>> Count(Expression<Func<Category, bool>> filter = null);
        Task<IDataResult<bool>> Any(Expression<Func<Category, bool>> filter);
    }
}
