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

    public async Task<GeneralView> Insert(GeneralView generalView)
    {
      General general = new General();
      general.type = generalView.type;
      general.code = generalView.code;
      general.link = generalView.link;
      _context.Insert(general);

      await UnitOfWork.SaveChangesAsync();

      return generalView;
    }

    public async Task<GeneralView> Search(string type = null, string code = null, string link = null, int page = 1, int pageSize = pageSize)
    {
      GeneralView rs = new GeneralView();
      rs.generals = new PagedListResult<Item>();
      rs.generals.Items = new List<Item>();

      Expression<Func<General, bool>> All = x => x.Id > 0;

      if (type != null)
      {
        Expression<Func<General, bool>> filter = x => x.type == type;
        All = All.And(filter);
      }
      if (code != null)
      {
        Expression<Func<General, bool>> filter = x => x.code == code;
        All = All.And(filter);
      }
      if (link != null)
      {
        Expression<Func<General, bool>> filter = x => x.link == link;
        All = All.And(filter);
      }

      PagedListResult<General> results = await _dataProvider.ListAsync(All, null, true, page, pageSize);
      //results.Items = results.Items.OrderByDescending(x => x.Id).ToList();

      foreach (var result in results.Items)
      {
        Item item = new Item();
        item.type = result.type;
        item.code = result.code;
        item.link = result.link;
        rs.generals.Items.Add(item);
      }
      rs.generals.PageCount = results.PageCount;
      rs.generals.TotalCount = results.TotalCount;

      return rs;
    }
  }
}
