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




namespace sahand.Controllers;
 [Area("Admin")]
public class AdminHeaderController : Controller
{
   
    private readonly ILogger<AdminHeaderController> _logger;
    private readonly Application db;
    private readonly IWebHostEnvironment en;
    public AdminHeaderController(ILogger<AdminHeaderController> logger,Application _db,IWebHostEnvironment _en)
    {
        _logger = logger;
        db =_db;
        en=_en;
    }

    public IActionResult show(string Id)
    {
        var find =db.tbl_Headers.ToList();
        if (find != null)
        {
            ViewBag.find =find;
        }
         if (Id != null)
        {
            ViewBag.message =Id;
        }
        return View();
    }
public IActionResult GotoUpdata(int Id)
{
    var find=db.tbl_Headers.SingleOrDefault(p=>p.Id == Id);
    if (find !=null)
    {
      Vm_Header s=new Vm_Header();
        s.Vm_Id=find.Id;
        s.Vm_Title=find.Title;
        s.Vm_Detail=find.Detail;
        s.Vm_image=find.image;
        return View(s);
    }
    return RedirectToAction("show", new{Id="مورد انتخابی وجود ندارد"});
}
public async  Task<IActionResult> Update(Vm_Header n)
{
    if (n !=null)
    {
        var find=db.tbl_Headers.SingleOrDefault(p=>p.Id==n.Vm_Id);
        find.Title=n.Vm_Title;
        find.Detail=n.Vm_Detail;
        if (n.Vm_img !=null)
        {
          string Pathdel = $"{en.WebRootPath}\\fileupload\\{n.Vm_image}";
          FileInfo file = new FileInfo(Pathdel);
          if (file.Exists)
          {
              file.Delete();
          }
          string FileExtension1 = Path.GetExtension(n.Vm_img.FileName);
          string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
          var path1 = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
          using (var stream = new FileStream(path1, FileMode.Create))
          {
            await n.Vm_img.CopyToAsync(stream);
          }
          n.Vm_image = NewFileName;
        }
        db.tbl_Headers.Update(find);
        db.SaveChanges();
    }
    
    return RedirectToAction("show", new{Id="عملیات ویرایش با موفقیت انجام شد"});
}


}
