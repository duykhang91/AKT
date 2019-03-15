using AKTTool.Database;
using AKTTool.Models;
using AKTTool.Repository;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public class DatabaseServices : ServiceBase, IDatabaseServices
  {
    private IDoorRepository _context;

    public DatabaseServices(IUnitOfWork unitOfWork, AppDbContext appDbContext, IDoorRepository context) : base(unitOfWork, appDbContext)
    {
      _context = context;
    }

    public async Task<Door> insert(Door doors)
    {
      General door = new General();
      door.type = doors.input.type;
      _context.Insert(door);

      await UnitOfWork.SaveChangesAsync();

      return doors;
    }
  }
}
