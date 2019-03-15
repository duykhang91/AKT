using AKTTool.Models;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public interface IDatabaseServices
  {
    Task<Door> Insert(Door doors);
    Task<Door> GetList(int page, int pageSize);
  }
}
