using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AKTTool.Models;
using AKTTool.Services;
using System;
using System.Linq;

namespace AKTApp.Controllers
{
  public class SearchController : Controller
  {
    private readonly IDatabaseServices _databaseServices;
    private readonly int pageSize = 20;

    public SearchController(IDatabaseServices databaseServices)
    {
      _databaseServices = databaseServices;
    }

    // GET: Search
    public async Task<ActionResult> Index()
    {
      setViewBags();
      GeneralView generals = new GeneralView();
      generals = await _databaseServices.SearchAsync(null, null, null, 1, pageSize);

      return View(generals);
    }

    [HttpPost]
    public async Task<ActionResult> Insert(GeneralView model)
    {
      setViewBags();

      await _databaseServices.InsertOrUpdateAsync(model.itemInsert);

      if (ModelState.IsValid)
      {
        model = await _databaseServices.SearchAsync(null, null, null, 1, pageSize);
        ViewBag.ShowModal = "success";
      }

      return PartialView("_Result", model);
    }

    [HttpPost]
    public async Task<ActionResult> Search(GeneralView model, string page = null)
    {
      setViewBags();
      if (page == null) { page = "1"; }
      int pageIndex = int.Parse(page);
      ViewBag.Page = pageIndex;

      model = await _databaseServices.SearchAsync(model.item.type, model.item.code, model.item.link, pageIndex, pageSize);

      return PartialView("_Result", model);
    }

    public async Task<ActionResult> Update(GeneralView model, string update)
    {
      setViewBags();
      int pageIndex = 1;
      if (update != null)
      {
        string method = new String(update.Where(Char.IsLetter).ToArray());
        update = update.Replace(method, "#");

        int id = int.Parse(update.Substring(0, update.IndexOf('#')));
        pageIndex = int.Parse(update.Substring(update.IndexOf('#') + 1, update.Length - update.IndexOf('#') - 1));
        ViewBag.Page = pageIndex;

        switch (method)
        {
          case "update":
            if (ModelState.IsValid)
            {
              await _databaseServices.InsertOrUpdateAsync(model.itemInsert);
            }
            else
            {
              ViewBag.Edit = id;
            }
            break;
          case "delete":
            bool isDeteled = await _databaseServices.DeleteAsync(id);
            if (isDeteled)
            {
              ViewBag.ShowModal = "deleted#" + id;
            }
            break;
          case "edit":
            ViewBag.Edit = id;
            break;
        }
      }

      model = await _databaseServices.SearchAsync(null, null, null, pageIndex, pageSize);

      if (model.generals.Items.Count == 0)
      {
        pageIndex--;
        ViewBag.Page = pageIndex;
        model = await _databaseServices.SearchAsync(null, null, null, pageIndex, pageSize);
      }

      return PartialView("_Result", model);
    }

    public void setViewBags()
    {
      ViewBag.Page = 1;
      ViewBag.ShowModal = null;
    }
  }
}