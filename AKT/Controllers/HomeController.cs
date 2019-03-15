using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AKT.Models;
using AKTTool.Models;
using AKTTool.Services;

namespace AKT.Controllers
{
  public class HomeController : Controller
  {
    private readonly IDatabaseServices _databaseServices;

    public HomeController(IDatabaseServices databaseServices)
    {
      _databaseServices = databaseServices;
    }

    public ActionResult Index()
    {
      return View();
    }

      public ActionResult Insert(Door model)
    {
      _databaseServices.insert(model);

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
