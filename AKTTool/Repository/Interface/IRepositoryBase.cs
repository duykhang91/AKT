﻿using AKTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AKTTool.Repository
{
  public interface IRepositoryBase<TEntity> where TEntity : EntityBase
  {
    Task<TEntity> GetByIdAsync(int id);

    void Insert(TEntity entity);

    void InsertRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Delete(int id);

    void Delete(TEntity entity);

    IFluentQuery<TEntity> Query(IQueryObject<TEntity> queryObject);

    IFluentQuery<TEntity> Query(Expression<Func<TEntity, bool>> query);

    IFluentQuery<TEntity> Query();
  }
}
