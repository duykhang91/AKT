using AKTTool.Common;
using AKTTool.Database;
using AKTTool.Models;
using AKTTool.Repository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace AKTTool.Services
{
  public class DatabaseServices : ServiceBase, IDatabaseServices
  {
    private IDoorRepository _context;
    private IDataProvider<General> _dataProvider;
    private const int pageSize = 50;

    public DatabaseServices(IUnitOfWork unitOfWork, AppDbContext appDbContext, IDoorRepository context, IDataProvider<General> dataProvider) : base(unitOfWork, appDbContext)
    {
      _context = context;
      _dataProvider = dataProvider;
    }

    public async Task<General> InsertOrUpdateAsync(ItemInsert itemInsert)
    {
      General general = new General();
      if (itemInsert.id > 0)
      {
        general = mapper(itemInsert);

        _context.Update(general);
      }
      else
      {
        general = new General();
        general = mapper(itemInsert);

        _context.Insert(general);
      }

      await UnitOfWork.SaveChangesAsync();

      return general;
    }

    private General mapper(ItemInsert item)
    {
      General result = new General();
      result.id = item.id;
      result.type = item.type;
      result.code = item.code;
      result.link = item.link;

      return result;
    }

    public async Task<GeneralView> SearchAsync(string type = null, string code = null, string link = null, int page = 1, int pageSize = pageSize)
    {
      GeneralView rs = new GeneralView();
      rs.generals = new PagedListResult<Item>();
      rs.generals.Items = new List<Item>();

      Expression<Func<General, bool>> All = x => x.id > 0;

      if (type != null)
      {
        Expression<Func<General, bool>> filter = x => x.type.Contains(type);
        All = All.And(filter);
      }
      if (code != null)
      {
        Expression<Func<General, bool>> filter = x => x.code.Contains(code);
        All = All.And(filter);
      }
      if (link != null)
      {
        Expression<Func<General, bool>> filter = x => x.link.Contains(link);
        All = All.And(filter);
      }

      PagedListResult<General> results = await _dataProvider.ListAsync(All, null, true, page, pageSize);
      //results.Items = results.Items.OrderByDescending(x => x.Id).ToList();

      foreach (var result in results.Items)
      {
        Item item = new Item();
        item.id = result.id;
        item.type = result.type;
        item.code = result.code;
        item.link = result.link;
        rs.generals.Items.Add(item);
      }
      rs.generals.PageCount = results.PageCount;
      rs.generals.TotalCount = results.TotalCount;

      return rs;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      _context.Delete(id);

      await UnitOfWork.SaveChangesAsync();

      if (await _context.GetByIdAsync(id) != null)
      {
        return false;
      }

      return true;
    }
  }
}
