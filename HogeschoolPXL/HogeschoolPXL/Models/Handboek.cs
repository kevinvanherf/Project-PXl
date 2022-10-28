using HogeschoolPXL.ModelValidations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogeschoolPXL.Models
{
    public class Handboek
    {
        public int HandboekID { get; set; }
        public string Titel { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal KostPrijs { get; set; }
        [UitGifteDate]
        public DateTime UitgifteDatum { get; set; }
        public string Afbeelding { get; set; }

    }
}
