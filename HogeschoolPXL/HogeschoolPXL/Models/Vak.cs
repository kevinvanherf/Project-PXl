using HogeschoolPXL.ModelValidations;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Vak
    {
        public int VakId { get; set; }
        [Required]
        public string? VakNaam { get; set; }
        [Required]
        public int StudiePunten { get; set; }
        [CursussenCreate]
        public int? HandboekID { get; set; }

        public Handboek? Handboek { get; set; }

    }
}
