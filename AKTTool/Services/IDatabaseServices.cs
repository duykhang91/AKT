using AKTTool.Models;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public interface IDatabaseServices
  {
    Task<GeneralView> Insert(GeneralView doors);
    Task<GeneralView> Search(string type = null, string code = null, string link = null, int page = 1, int pageSize = 1);
  }
}
