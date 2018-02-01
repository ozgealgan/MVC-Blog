using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Controllers
{
    public class MakaleController : Controller
    {

		blogSitemEntities db = new blogSitemEntities();
		// GET: Makale
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Detay(int id)
		{
			Makale m = db.Makales.FirstOrDefault(x => x.id == id);
			ViewBag.resim = db.Resims.FirstOrDefault(x => x.MakaleID == id);
			return View(m);
		}

		public JsonResult YorumYap(string icerik, string kulAdi, string kulMail, int makaleId)
		{
			if(icerik!=null && kulAdi!=null && kulMail!=null)
			{
				db.Yorums.Add(new Yorum { MakaleID = makaleId, YorumIcerik = icerik, YorumYapanAdi = kulAdi, YorumYapanMail = kulMail, Tarih = DateTime.Now });
				db.SaveChanges();
			}
			return Json(false, JsonRequestBehavior.AllowGet);
		}
	}
}