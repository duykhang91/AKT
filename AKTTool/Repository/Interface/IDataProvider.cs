using AKTTool.Common;
using AKTTool.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AKTTool.Repository
{
  public interface IDataProvider<T> where T : EntityBase
  {
    Task<T> GetByIdAsync(int id);

    Task<PagedListResult<T>> ListAsync(Expression<Func<T, bool>> filter = null, string sortData = null, bool includeDependents = false, int? pageIndex = null, int? pageSize = null);
  }
}
