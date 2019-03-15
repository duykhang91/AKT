using AKTTool.Database;
using AKTTool.Models;

namespace AKTTool.Repository
{
  public class DoorRepository : RepositoryBase<General>, IDoorRepository
  {
    public DoorRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
  }
}
