using Microsoft.Ajax.Utilities;
using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace myBlog.Controllers
{
    public class AdminController : Controller
    {
		blogSitemEntities db = new blogSitemEntities();
	
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


		[AllowAnonymous]
		public ActionResult Login()
		{
			
			return View();
		}


		[HttpPost]
		public ActionResult Login(string kulAdi, string parola)
		{
			if (db.Admins.Any(x => x.KullaniciAdi==kulAdi && x.Parola==parola))
			{
				return RedirectToAction("Index", "Admin");
			}
			else
			{
				ViewBag.Mesaj = "Kullanıcı adı veya parola hatalı!";
				return View();
			}
		}

		public ActionResult MakaleOlustur()
		{
			ViewBag.KategoriId = new SelectList(db.Kategoris, "id", "Adi");
			return View();
		}

		[HttpPost]
		public ActionResult MakaleOlustur(Makale makale, string etiketler, HttpPostedFileBase resim )
		{
			if(makale!=null)
			{ 
				
				makale.YayınlanmaTarihi = DateTime.Now;
				db.Makales.Add(makale);
				db.SaveChanges();
				if (resim != null)
					ResimKaydet(resim, makale.id);

				string[] etikets = etiketler.Split(',');
				foreach (string etiket in etikets)
				{
					Etiket etk = db.Etikets.FirstOrDefault(x => x.Adi.ToLower() == etiket.ToLower().Trim());
					if (etk==null)
					{
						etk = new Etiket();
						etk.Adi = etiket;
						db.Etikets.Add(etk);
						db.SaveChanges();
					}
					makale.Etikets.Add(etk);
					db.SaveChanges();
					
						
					
				}

				return RedirectToAction("AdminMakaleListele");
			}
			else
			{
				return View();
			}
			
		}

		private void ResimKaydet(HttpPostedFileBase Resim, int makaleID)
		{
			int kucukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
			int kucukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
			int ortaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]); 
			int ortaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
			int buyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
			int buyukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);


			string newName = Path.GetFileNameWithoutExtension(Resim.FileName)+ "-" //Remin orjinal adı 
				+Guid.NewGuid()+Path.GetExtension(Resim.FileName); // Guid ile benzersiz bir isim verilir.
																   // GetExtension ile remin uzantısı alınır.

			Image orjresim = Image.FromStream(Resim.InputStream);
			Bitmap kucukRes = new Bitmap(orjresim, kucukWidth, kucukHeight);
			Bitmap ortaRes = new Bitmap(orjresim, ortaWidth, ortaHeight);
			Bitmap buyukRes = new Bitmap(orjresim, buyukWidth, buyukHeight);

			kucukRes.Save(Server.MapPath("~/Content/Resimler/Kucuk" + newName));
			ortaRes.Save(Server.MapPath("~/Content/Resimler/Orta" + newName));
			buyukRes.Save(Server.MapPath("~/Content/Resimler/Buyuk" + newName));

			Resim dbResim = new Models.Resim();
			dbResim.Adi = Resim.FileName;
			dbResim.Buyuk = "/Content/Resimler/Buyuk"+ newName;
			dbResim.Orta = "/Content/Resimler/Orta" + newName;
			dbResim.Kucuk = "/Content/Resimler/Kucuk" + newName;
			dbResim.MakaleID = makaleID;
			db.Resims.Add(dbResim);
			db.SaveChanges();

		}

		public ActionResult MakaleDuzenle()
		{
			return View();
		}

		public ActionResult AdminMakaleListele()
		{

			var makaleList = db.Makales.ToList();
			return View(makaleList);
		}
	
		public ActionResult MakaleSil(int id)
		{
			var makale = db.Makales.Where(x => x.id == id).SingleOrDefault();
			if(makale==null)
			{
				return HttpNotFound();
			}
			return View(makale);
		}

		[HttpPost]
		public ActionResult MakaleSil(int id, FormCollection c)
		{
			var makale = db.Makales.Where(x => x.id == id).SingleOrDefault();
			if (makale == null)
			{
				return HttpNotFound();
			}
			foreach (var y in makale.Yorums.ToList())
			{
				db.Yorums.Remove(y);
			}
			foreach (var e in makale.Etikets.ToList())
			{
				db.Etikets.Remove(e);
			}
			foreach (var r in makale.Resims.ToList())
			{
				db.Resims.Remove(r);
			}
			db.Makales.Remove(makale);
			db.SaveChanges();
			return View("Index");
		}
	}
}