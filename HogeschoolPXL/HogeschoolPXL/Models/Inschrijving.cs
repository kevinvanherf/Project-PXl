using HogeschoolPXL.ModelValidations;
using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Inschrijving
    {
        public int InschrijvingId { get; set; }
        [InschrijvingControle]
        public int StudentId { get; set; }
        [InschrijvingControle]
        public int VakLectorId { get; set; }
        [Required]
        public int AcademieJaarId { get; set; }

        public VakLector? vakLector { get; set; }
        public Student? Student { get; set; }
        public AcademieJaar? academieJaar { get;set; }
    }
}
