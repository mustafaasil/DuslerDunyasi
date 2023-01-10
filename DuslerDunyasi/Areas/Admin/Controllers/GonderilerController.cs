using DuslerDunyasi.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Permissions;

namespace DuslerDunyasi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GonderilerController : Controller
    {
        private readonly UygulamaDbContext _db;

        public GonderilerController(UygulamaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string? durum)
        {
            ViewBag.Mesaj = durum == "eklendi" ? "Gönderi başarıyla oluşturuldu" :
                            durum == "duzenlendi" ? "Gönderi başarıyla güncellendi" :
                            durum == "silindi" ? "Gönderi başarıyla silindi" : null;

            return View(_db.Gonderiler.Include(x => x.Kategori).ToList()); //Inculude ile ilişkili oldugu kategorileri getir
        }

        public IActionResult Yeni()
        {
            KategorileriYukle();
            return View("Yonet");
        }

        private void KategorileriYukle()
        {
            ViewBag.Kategoriler = _db.Kategoriler.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Ad }).ToList();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(GonderiViewModel vm) // veri tabanı gonderi istediği ,için biz burda newledik
        {
            if (ModelState.IsValid)
            {
                var gonderi = new Gonderi()
                {
                    
                    Baslik =vm.Baslik,
                    Icerik = vm.Icerik,
                    KategoriId = vm.KategoriId!.Value
                };
                _db.Gonderiler.Add(gonderi);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { durum = "eklendi" });
            }

            KategorileriYukle();
            return View("Yonet");
        }

        public IActionResult Duzenle(int id)
        {
            var gonderi = _db.Gonderiler.Find(id);

            if (gonderi == null)
                return NotFound();

            var vm = new GonderiViewModel()
            {
                Baslik = gonderi.Baslik,
                Icerik = gonderi.Icerik,
                KategoriId = gonderi.KategoriId,
                Id = gonderi.Id

            };

            KategorileriYukle();
            return View("Yonet", vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Duzenle(GonderiViewModel vm) 
        {
            if (ModelState.IsValid)
            {
                var gonderi = _db.Gonderiler.Find(vm.Id);
                if (gonderi == null)
                    return NotFound();

                gonderi.Baslik = vm.Baslik;
                gonderi.Icerik = vm.Icerik;
                gonderi.KategoriId = vm.KategoriId!.Value;
                gonderi.DegistirilmeZamani = DateTime.Now;
                
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { durum = "duzenlendi" });
            }

            KategorileriYukle();
            return View("Yonet");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Sil(int id)
        {
            var gonderi = _db.Gonderiler.Find(id);

             if (gonderi == null)
                return NotFound();

            _db.Gonderiler.Remove(gonderi);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index), new { durum = "silindi" });
        }
    }
}
