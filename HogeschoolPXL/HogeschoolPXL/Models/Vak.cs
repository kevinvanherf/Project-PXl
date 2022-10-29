using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Vak
    {
        public int VakId { get; set; }
        [Required]
        public string VakNaam { get; set; }
        [Required]
        public int StudiePunten { get; set; }
        [Required]
        public int HandboekID { get; set; }

        public ICollection<Handboek> Handboeks { get; set; }

    }
}
