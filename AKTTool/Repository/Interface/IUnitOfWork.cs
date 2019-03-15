using AKTTool.Models;
using System.Threading;
using System.Threading.Tasks;
using AKTTool.Common;

namespace AKTTool.Repository
{
  public interface IUnitOfWork
  {
    Task SaveChangesAsync(ConcurrencyResolutionStrategy strategy = ConcurrencyResolutionStrategy.None);

    Task SaveChangesAsync(CancellationToken cancellationToken, ConcurrencyResolutionStrategy strategy = ConcurrencyResolutionStrategy.None);

    /// <summary>
    /// Forces reloading data from database
    /// </summary>
    T Reload<T>(T item) where T : EntityBase;

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();

    //IRepository<T> RepositoryBase<T>() where T : EntityBase;
  }
}
