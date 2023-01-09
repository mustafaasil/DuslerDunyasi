using System.ComponentModel.DataAnnotations;

namespace DuslerDunyasi.Data
{
    public class Gonderi
    {
        public int Id { get; set; }

        [Display(Name ="Başlık")]
        [Required(ErrorMessage ="{0} alanı zorunludur")] // şuan 0 yerine üsütüneki ilk display oldugu için gelir
        [MaxLength(400)]
        public string Baslik { get; set; } = null!;

        [Display(Name = "İçerik")]
        public string? Icerik { get; set; } = "";
        

        [MaxLength(255)]
        public string? Resim { get; set; }

        public DateTime OlusturulmaZamani { get; set; } = DateTime.Now;
        public DateTime DegistirilmeZamani { get; set; } = DateTime.Now;

        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; } = null!;
    }
}
