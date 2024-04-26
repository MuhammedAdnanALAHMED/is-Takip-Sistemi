using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFirmaCagri.Models.Entity;

namespace MvcFirmaCagri.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        
        public ActionResult AktifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrilar = db.TblCagrilar.Where(x=>x.Durum==true &&x.CagriFirma==id).ToList();
            return View(cagrilar);
        }
    
        
        public ActionResult PasifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var cagrilar = db.TblCagrilar.Where(x => x.Durum == false && x.CagriFirma == id).ToList();
            return View(cagrilar);
        }
        [HttpGet]
        public ActionResult YeniCagri()
        {
             return View();
        }
        [HttpPost]
        public ActionResult YeniCagri(TblCagrilar p)
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            p.Durum = true;
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CagriFirma = id;
            db.TblCagrilar.Add(p);
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
      
        public ActionResult CagriDetay(int id)
        {
            var cagri = db.TblCagriDetay.Where(x => x.Cagri == id).ToList();
            return View(cagri);
        }
        public ActionResult CagriGetir(int id)
        {
            var cagri = db.TblCagrilar.Find(id);
            return View("CagriGetir",cagri);
        }
        public ActionResult CagriDuzenle(TblCagrilar p)
        {
            var cagri = db.TblCagrilar.Find(p.ID);
            cagri.Konu = p.Konu;
            cagri.Aciklama = p.Aciklama;
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
        //public ActionResult ProfilimiDetay(int id)
        //{
        //    var firma = db.TblFirmalar.Where(x => x.ID == id).ToList();
        //    return View(firma);
        //}
        //public ActionResult ProfilimiGetir(int id)
        //{
        //    var firma = db.TblCagrilar.Find(id);
        //    return View("ProfilimİGetir", firma);
        //}
        public ActionResult progiter()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
          
           
            if (mail == "admin@gmail.com" && id == 15)
            {
                var pro = db.TblFirmalar.ToList();
                return View("progiter", pro);
            }
            else
            {
                var pro = db.TblFirmalar.Where(x => x.ID == id).ToList();
                return View("progiter", pro);
            }
        }
      

        public ActionResult Edit(int id)
        {
            var firma = db.TblFirmalar.Where(s => s.ID == id).FirstOrDefault();
            UpdateModel<TblFirmalar>(firma);
            db.SaveChanges();
            
            return View(firma);
        }
        public ActionResult sifreunuttum(int id)
        {
            var firma = db.TblFirmalar.Where(s => s.ID == id).FirstOrDefault();
            UpdateModel<TblFirmalar>(firma);
            db.SaveChanges();

            return View(firma);
        }
        public ActionResult Delete(int id)
        {
            if (id== 15)
                {

              
                var f = db.TblFirmalar.Where(s => s.ID == id).FirstOrDefault();
               
                db.TblFirmalar.Remove(f);
                db.SaveChanges();
                return View(f);
                }
            else
            {
                return RedirectToAction("progiter");
            }

        }
        //public ActionResult ProfilDuzenle(TblFirmalar f)
        //{


        //    var firma = db.TblFirmalar.Find(f.ID);
        //    firma.ID = f.ID;
        //    firma.Ad = f.Ad;
        //    firma.Yetkili = f.Yetkili;
        //    firma.Telefon = f.Telefon;
        //    firma.Mail = f.Mail;
        //    firma.Sektor = f.Sektor;
        //    firma.il = f.il;
        //    firma.ilce = f.ilce;
        //    firma.Adres = f.Adres;
        //    firma.Gorsel = f.Gorsel;
        //    firma.Sifre = f.Sifre;
        //    db.Entry(firma).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("AktifCagrilar");
        //    //var mail = (string)Session["Mail"];
        //    //var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
        //    //var mail = (string)Session["Mail"];
        //    //var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
        //    ////    //var profil = db.TblFirmalar.Where(x=>x.ID==id).FirstOrDefault();
        //    //    //db.SaveChanges();
        //    //    //return View(profil);

        //}
        public ActionResult AnaSayfa()
        {

            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var toplamcagri = db.TblCagrilar.Where(x => x.CagriFirma == id).Count();
            var aktifcagri = db.TblCagrilar.Where(x => x.CagriFirma == id&&x.Durum==true).Count();
            var pasifcagri = db.TblCagrilar.Where(x => x.CagriFirma == id&&x.Durum==false).Count();
            var yetkili = db.TblFirmalar.Where(x => x.ID == id).Select(y=>y.Yetkili).FirstOrDefault();
            var sektor = db.TblFirmalar.Where(x => x.ID == id).Select(y=>y.Sektor).FirstOrDefault();
            var firmaadi = db.TblFirmalar.Where(x => x.ID == id).Select(y=>y.Ad).FirstOrDefault();
            var firmagorsel = db.TblFirmalar.Where(x => x.ID == id).Select(y=>y.Gorsel).FirstOrDefault();
            
                ViewBag.c1= toplamcagri;
                ViewBag.c2= aktifcagri;
                ViewBag.c3= pasifcagri;
                ViewBag.c4= yetkili;
                ViewBag.c5= sektor;
                ViewBag.c6= firmaadi;
                ViewBag.c7= firmagorsel;
            return View();
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var mesajlar = db.TblMesajlar.Where(x => x.Alici == id && x.Durum==true).ToList();
            var mesajsayisi = db.TblMesajlar.Where(x => x.Alici == id && x.Durum==true).Count();
            ViewBag.m1 = mesajsayisi;
            return PartialView(mesajlar);
        }
        public PartialViewResult Partial2()
        {
            var mail = (string)Session["Mail"];
            var id = db.TblFirmalar.Where(x => x.Mail == mail).Select(y=>y.ID).FirstOrDefault();
            var cagrilar = db.TblCagrilar.Where(x => x.CagriFirma== id && x.Durum == true).ToList();
            var cagrisayisi = db.TblCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            ViewBag.n1 = cagrisayisi;
            return PartialView(cagrilar); 
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public PartialViewResult Partial3()
        {
            return PartialView();
        }
       


    }
}