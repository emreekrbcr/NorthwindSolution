using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Business.Abstract;
using Business.Utilities.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),
                Messages.CategoryMessages.SuccessMessages.CategoriesListed);
        }

        public IDataResult<int> Count(Expression<Func<Category, bool>> filter = null)
        {
            return new SuccessDataResult<int>(_categoryDal.Count(filter), Messages.CommonMessages.OperationSucceeded);
        }

        public IDataResult<bool> Any(Expression<Func<Category, bool>> filter)
        {
            return new SuccessDataResult<bool>(_categoryDal.Any(filter), Messages.CommonMessages.OperationSucceeded);
        }
    }
}