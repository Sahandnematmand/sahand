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
namespace sahand.Controllers;
 [Area("Admin")]
public class AdminCompanyController : Controller
{

    private readonly ILogger<AdminCompanyController> _logger;
    private readonly Application db;
    private readonly IWebHostEnvironment en;
    public AdminCompanyController(ILogger<AdminCompanyController> logger,Application _db,IWebHostEnvironment _en)
    {
        _logger = logger;
        db =_db;
        en=_en;
    }
    public IActionResult Show(string id)
    {
        var find =db.tbl_Companies.ToList();
        if (find !=null)
        {
            ViewBag.Company =find;
        }
        if (id != null)
        {
            ViewBag.message = id;
        }
        return View();
    }
    public IActionResult Gotoadd()
    {
        return View();
    }
    
     public async  Task<IActionResult> add(Vm_Company s)
    {
        if (s !=null)
        {
            Tbl_Company n=new Tbl_Company();
            n.Title=s.Vm_Title;
            n.status=true;
            n.image="";
            if (s .Vm_img != null)
                {
                    string FileExtension1 = Path.GetExtension(s.Vm_img.FileName);
                    string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
                    var path1 = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        await s.Vm_img.CopyToAsync(stream);
                    }
                    n.image=NewFileName;
                }
                db.tbl_Companies.Add(n);
                db.SaveChanges();
                return RedirectToAction("show" , new{id="عملیات افزودن شرکت با موفقیت انجام شد"});
                
        }else
        {
            
                return RedirectToAction(" Gotoadd" , new{id="لطفا مجددا تلاش  کنید"});
        }
        
    }
    public IActionResult Gotostatus(int id)
    {
        var find=db.tbl_Companies.SingleOrDefault(p=>p.Id==id);
        if (find !=null)
        {
            if(find.status ==true)
            {
                find.status=false;
            }else
            {
                find.status=true;
            }
            db.tbl_Companies.Update(find);
            db.SaveChanges();
             return RedirectToAction("show" , new{id="  عملیات تغییر وضعیت با موفقیت انجام شد "});
        }else
        
        {
        return RedirectToAction("show" , new{id="مورد انتخابی وجود ندارد"});
        }   
    }
    public IActionResult Gotodelete(int id)
    {
        var find=db.tbl_Companies.SingleOrDefault(p=>p.Id == id);
        if (find !=null)
        {
            db.Remove(find);
            db.SaveChanges();
            return RedirectToAction("show" , new{id="  عملیات حذف با موفقیت انجام شد "});
        }else
        {

        return RedirectToAction("show" , new{id="مورد انتخابی وجود ندارد"});
        }
    }
    public IActionResult Gotoupdata(int id)
    {
        
        var find=db.tbl_Companies.SingleOrDefault(p=>p.Id == id);
        if(find != null)
        {
            Vm_Company a = new Vm_Company();
            a.Vm_Id = find.Id;
            a.Vm_image = find.image;
            a.Vm_Title = find.Title;
        }
        return View();
    }
   public async  Task<IActionResult>updata(Vm_Company f)
    {
    if (f !=null)
    {
        var find =db.tbl_Companies.SingleOrDefault(p=>p.Id  == f.Vm_Id);
        find.Title=f.Vm_Title;
        if (f.Vm_image != null)
        {
         string FileExtension1 = Path.GetExtension(f.Vm_img.FileName);
         string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
         var path1 = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
         using (var stream = new FileStream(path1, FileMode.Create))
         {
             await f.Vm_img.CopyToAsync(stream);
         }
         find.image=NewFileName;
        }
        db.Update(find);
        db.SaveChanges();
        return RedirectToAction("show" , new{id="  عملیات ویرایش شرکت با موفقیت انجام شد "});
    }else
    {
        
        return RedirectToAction("Gotoupdata" , new{id="لطفا مجددا تلاش کنیذپد"});
    }
    }
    
}





