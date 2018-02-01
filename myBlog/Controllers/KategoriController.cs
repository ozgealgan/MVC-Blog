using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Controllers
{
    public class KategoriController : Controller
    {
		blogSitemEntities db = new blogSitemEntities();
		
		
		// GET: Kategori
		public ActionResult Index(int id)
		{	
			return View(id);
		}

		public ActionResult MakaleleriGetir(int id)
		{
			var data = db.Makales.Where(x => x.KategoriID == id);
			return View("MakaleListele", data);
		}
	}
}