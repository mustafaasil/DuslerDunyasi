using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DuslerDunyasi.Areas.Admin.Models
{
    public class GonderiViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} alanı zorunludur")] // şuan 0 yerine üsütüneki ilk display oldugu için gelir
        [MaxLength(400)]
        public string Baslik { get; set; } = null!;

        [Display(Name = "İçerik")]
        public string? Icerik { get; set; } = "";


        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        public int? KategoriId { get; set; }
    }
}
