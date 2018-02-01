using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace myBlog.Controllers
{
    public class HomeController : Controller
    {
		// GET: Home
		blogSitemEntities db = new blogSitemEntities();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult CategoryGetir()
		{
			return View(db.Kategoris.ToList());
		}

		public ActionResult PopulerGetir()
		{
			ViewBag.Populer = db.Makales.OrderByDescending(x => x.Goruntulenme).Take(3);
			return View();

		}
		public ActionResult SonGetir()
		{
			ViewBag.SonGonderi = db.Makales.OrderByDescending(x => x.YayınlanmaTarihi).Take(3);
			return View();
		}

		public ActionResult EtiketGetir()
		{
			var tags = db.Etikets.ToList();
			return View(tags);
		}

		public ActionResult TumMakaleleriGetir()
		{
			var makales = db.Makales.ToList();
			ViewBag.resim = db.Resims.ToList();
			return View("MakaleListele", makales);
		}

		public ActionResult Hakkimda()
		{
			ViewBag.hakkimda = db.Admins.FirstOrDefault(x=> x.id==1);
			return View();
		}
	}
}