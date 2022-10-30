using HogeschoolPXL.ModelValidations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogeschoolPXL.Models
{
    public class Handboek
    {
        public int HandboekID { get; set; }
        [Required]
        public string Titel { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal? KostPrijs { get; set; }
        [UitGifteDate]
        public DateTime UitgifteDatum { get; set; }
        [Required]
        public string Afbeelding { get; set; }

    }
}
