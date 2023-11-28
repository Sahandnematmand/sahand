using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sahand.Models;

namespace sahand.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Application db;

    public HomeController(ILogger<HomeController> logger,Application _db)
    {
        _logger = logger;
        db= _db;
    }

    public IActionResult Index()
    {
        var header=db.tbl_Headers.ToList();
        if (header !=null)
        {
            ViewBag.header=header;
        }
        var companys=db.tbl_Companies.OrderByDescending(p=>p.Id).Where(p=>p.status ==true).ToList();
        if (companys != null)
        {
            ViewBag.company= companys;
        }
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
