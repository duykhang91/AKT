using AKTTool.Models;
using System.Threading.Tasks;

namespace AKTTool.Services
{
  public interface IDatabaseServices
  {
    Task<Door> insert(Door doors);
  }
}
