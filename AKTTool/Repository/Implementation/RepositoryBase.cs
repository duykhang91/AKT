using AKTTool.Database;
using AKTTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AKTTool.Repository
{
  public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase, new()
  {
    protected readonly AppDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected RepositoryBase(AppDbContext dbContext)
    {
      DbContext = dbContext;
      DbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
      var query = DbSet.Where(x => x.Id == id);

      return await query.FirstOrDefaultAsync();
    }

    public virtual void Insert(TEntity entity)
    {
      if (entity != null)
      {
        DbSet.Add(entity);
      }
    }

    public virtual void InsertRange(IEnumerable<TEntity> entities)
    {
      if (entities != null)
      {
        var list = entities.ToList();
        DbSet.AddRange(list);
      }
    }

    public virtual void Update(TEntity entity)
    {
      if (entity != null)
      {
        DbSet.Update(entity);
      }
    }

    public virtual void Delete(int id)
    {
      var entity = new TEntity { Id = id };
      Delete(entity);
    }

    public virtual void Delete(TEntity entity)
    {
      if (entity == null)
        return;

      DbSet.Attach(entity);
      DbContext.Entry(entity).State = EntityState.Deleted;
      DbSet.Remove(entity);
    }

    public IFluentQuery<TEntity> Query()
    {
      return new FluentQuery<TEntity>(this);
    }

    public virtual IFluentQuery<TEntity> Query(IQueryObject<TEntity> queryObject)
    {
      return new FluentQuery<TEntity>(this, queryObject);
    }

    public virtual IFluentQuery<TEntity> Query(Expression<Func<TEntity, bool>> query)
    {
      return new FluentQuery<TEntity>(this, query);
    }

    internal virtual IQueryable<TEntity> GetQueryable()
    {
      return DbSet;
    }
  }
}
