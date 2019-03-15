using AKTTool.Common;
using AKTTool.Database;
using AKTTool.Models;
using AKTTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public class DatabaseServices : ServiceBase, IDatabaseServices
  {
    private IDoorRepository _context;
    private IDataProvider<General> _dataProvider;

    public DatabaseServices(IUnitOfWork unitOfWork, AppDbContext appDbContext, IDoorRepository context, IDataProvider<General> dataProvider) : base(unitOfWork, appDbContext)
    {
      _context = context;
      _dataProvider = dataProvider;
    }

    public async Task<Door> Insert(Door doors)
    {
      General door = new General();
      door.type = doors.input.type;
      _context.Insert(door);

      await UnitOfWork.SaveChangesAsync();

      return doors;
    }

    public async Task<Door> GetList(int page, int pageSize)
    {
      Door rs = new Door();
      rs.doors = new List<Item>();

      Expression<Func<General, bool>> All = x => x.Id > 0;

      PagedListResult<General> results = await _dataProvider.ListAsync(All, null, true, page, pageSize);

      foreach (var result in results.Items)
      {
        Item item = new Item();
        item.type = result.type;
        item.code = result.code;
        item.link = result.link;
        rs.doors.Add(item);
      }

      return rs;
    }
  }
}
