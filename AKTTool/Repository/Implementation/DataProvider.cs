using AKTTool.Common;
using AKTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AKTTool.Repository
{
  public class DataProvider<T> : IDataProvider<T> where T : EntityBase
  {
    private readonly IRepositoryBase<T> _repo;

    public DataProvider(IRepositoryBase<T> repo)
    {
      _repo = repo;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      var entity = await _repo.GetByIdAsync(id);
      return entity;
    }

    public async Task<PagedListResult<T>> ListAsync(Expression<Func<T, bool>> filter = null, string sortData = null, bool includeDependents = false, int? pageIndex = null, int? pageSize = null)
    {
      List<T> entities;

      // filtering
      var query = filter != null ? _repo.Query(filter) : _repo.Query();

      // order by
      if (sortData != null)
      {
        //var orderBy = TransformOrderByClause(sortData);
        query = query.OrderBy(sortData);
      }

      // paging
      int totalCount;
      int pageCount = 1;
      if (pageIndex != null && pageSize != null)
      {
        entities = await query.SelectPagingAsync(pageIndex.Value, pageSize.Value, out totalCount, out pageCount);
      }
      else
      {
        entities = await query.SelectAsync();
        totalCount = entities.Count();
      }

      // result
      var result = new PagedListResult<T>
      {
        TotalCount = totalCount,
        PageCount = pageCount,
        Items = entities
      };

      return result;
    }
  }
}
