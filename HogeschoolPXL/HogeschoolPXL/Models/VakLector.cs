using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class VakLector
    {
        public int vakLectorId { get; set; }
        [Required]
        public int LectorId { get; set; }
        [Required]
        public int VakId { get; set; }
        public Lector? Lector { get; set; }
        public Vak? vak { get; set; }
       
       
    }
}
