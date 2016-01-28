using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCapture.Library;

namespace WebCapture.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(String txtUrl,FormCollection form)
        {
            if (!String.IsNullOrEmpty(txtUrl))
            {
                var work = new WebToImg();
                var img = work.Capture(Utils.GetUrl(txtUrl));
                if (img != null)
                {
                    string fileName = Utils.GetFileName(txtUrl) +".jpg";

                    string savePath = Path.Combine(Server.MapPath("~/Thumb"), fileName);
                    if (System.IO.File.Exists(savePath))
                    {
                        fileName= Utils.GetFileName(txtUrl) +DateTime.Now.ToString("YYMMddHHmmss") +".jpg";
                        savePath = Path.Combine(Server.MapPath("~/Thumb"), fileName);
                    }
                    img.Save(savePath, ImageFormat.Jpeg);
                    return Json(new { code = "00", message = "Success", imgsrc = Url.Content("~/Thumb/" + fileName) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {code = "01", message = "Can not capture this website"});
                }

              
            }
            return View(); 
        }
    }
}
