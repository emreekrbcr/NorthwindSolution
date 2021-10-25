using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<int> Count(Expression<Func<Category, bool>> filter = null);
        IDataResult<bool> Any(Expression<Func<Category, bool>> filter);
    }
}
