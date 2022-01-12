using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async Task<IDataResult<List<Category>>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(await _categoryDal.GetAll(),
                Messages.CategoryMessages.SuccessMessages.CategoriesListed);
        }

        public async Task<IDataResult<int>> Count(Expression<Func<Category, bool>> filter = null)
        {
            return new SuccessDataResult<int>(await _categoryDal.Count(filter), Messages.CommonMessages.OperationSucceeded);
        }

        public async Task<IDataResult<bool>> Any(Expression<Func<Category, bool>> filter)
        {
            return new SuccessDataResult<bool>(await _categoryDal.Any(filter), Messages.CommonMessages.OperationSucceeded);
        }
    }
}