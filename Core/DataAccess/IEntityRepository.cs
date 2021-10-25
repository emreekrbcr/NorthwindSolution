﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    /// <summary>
    /// Base of CRUD Operations
    /// </summary>
    /// <typeparam name="T">Your entity</typeparam>
    public interface IEntityRepository<T>
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        int Count(Expression<Func<T, bool>> filter = null);
        bool Any(Expression<Func<T, bool>> filter);
    }
}
