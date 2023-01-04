using System.ComponentModel.DataAnnotations;

namespace DuslerDunyasi.Data
{
    public class Kategori
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ad { get; set; } = null!;

        public List<Gonderi> Gonderiler { get; set; } = new List<Gonderi>();

    }
}
