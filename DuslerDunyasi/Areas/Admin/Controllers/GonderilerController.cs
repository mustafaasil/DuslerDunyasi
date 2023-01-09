using DuslerDunyasi.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                            durum == "duzenlendi" ? "Gönderi başarıyla güncellenddi" :
                            durum == "silindi" ? "Gönderi başarıyla silindi" : null;

            return View(_db.Gonderiler.Include(x => x.Kategori).ToList()); //Inculude ile ilişkili oldugu kategorileri getir
        }

        public IActionResult Yeni()
        {
            return View("Yonet");
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
                    KategoriId = vm.KategoriId
                };
                _db.Gonderiler.Add(gonderi);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { durum = "eklendi" });
            }

            return View("Yonet");
        }

        public IActionResult Duzenle(int id)
        {
            return View("Yonet");
        }
    }
}
