using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class Student
    {
        [Required]
        public int StudentId { get; set; }
        public int GebruikerId { get; set; }

        public Gebruiker? Gebruiker { get; set; }
    }
}
