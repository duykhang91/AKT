using AKTTool.Models;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public interface IDatabaseServices
  {
    Task<General> InsertOrUpdateAsync(ItemInsert doors);
    Task<GeneralView> SearchAsync(string type = null, string code = null, string link = null, int page = 1, int pageSize = 1);
    Task<bool> DeleteAsync(int id);
  }
}
