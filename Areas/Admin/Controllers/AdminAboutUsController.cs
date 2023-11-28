using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sahand.Models;
using sahand.Models.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using sahand.Models.Database;
using Microsoft.EntityFrameworkCore;
namespace sahand.Controllers;
 [Area("Admin")]
public class AdminAboutUsController : Controller
{

    private readonly ILogger<AdminAboutUsController> _logger;
    private readonly Application db;
    private readonly IWebHostEnvironment en;
    public AdminAboutUsController(ILogger<AdminAboutUsController> logger,Application _db,IWebHostEnvironment _en)
    {
        _logger = logger;
        db =_db;
        en=_en;
    }
    public IActionResult show()
    {
     var find=db.tbl_AboutUs.OrderByDescending(p=>p.Id).ToList();
     if (find !=null)
     {
        ViewBag.Aboutus =find;
     }
        return View();
    }
    public IActionResult Gotoupdata(int Id)
    {
    var find=db.tbl_AboutUs.SingleOrDefault(p=>p.Id == Id);
    if (find !=null)
    {
        Vm__AboutUs a=new Vm__AboutUs();
        a.Vm_Id=find.Id;
        a.Vm_Title=find.Title;
        a.Vm_Detail=find.Detail;
        return View(a);
    }else
    {
        return RedirectToAction("show", new{Id="مورد انتخابی وجود ندارد"});
    }
    }
    public IActionResult updata(Vm__AboutUs a)
    {
        if (a !=null)
        {
            
         var find=db.tbl_AboutUs.SingleOrDefault(p=>p.Id ==  a.Vm_Id);
         find.Title=a.Vm_Title;
         find.Detail=a.Vm_Detail;
         db.tbl_AboutUs.Update(find);
         db.SaveChanges();
         
        return RedirectToAction("show", new{Id="عملیات موفق"});
        }else
        {
            
        return RedirectToAction("show", new{Id="مورد انتخابی وجود ندارد"});
        }
    }
    
    
    
    
}





