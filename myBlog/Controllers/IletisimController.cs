using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Controllers
{
    public class IletisimController : Controller
    {
		blogSitemEntities db = new blogSitemEntities();
		// GET: Iletisim
		public ActionResult Index()
        {
            return View();
        }

		public JsonResult MesajGonder(string mail, string name, string mesaj)
		{
			if(mail!=null && name!=null && mesaj != null)
			{
				Mesaj m = new Mesaj();
				m.gonderenMail = mail;
				m.gonderenAdi = name;
				m.mesaj1 = mesaj;
				m.tarih = DateTime.Now;
				db.Mesajs.Add(m);
				db.SaveChanges();
			}
			return Json(false, JsonRequestBehavior.AllowGet);
		}
    }
}