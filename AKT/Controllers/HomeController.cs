using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AKT.Models;
using AKTTool.Models;
using AKTTool.Services;
using System.Threading.Tasks;

namespace AKT.Controllers
{
  public class HomeController : Controller
  {
    private readonly IDatabaseServices _databaseServices;
    private const int pageSize = 50;

    public HomeController(IDatabaseServices databaseServices)
    {
      _databaseServices = databaseServices;
    }

    public async Task<ActionResult> Index()
    {
      Door door = new Door();
      door = await _databaseServices.GetList(1, pageSize);

      return View(door);
    }

      public async Task<ActionResult> Insert(Door model)
    {
      await _databaseServices.Insert(model);

      model = await _databaseServices.GetList(1, pageSize);

      return View("Index", model);
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
