using System.ComponentModel.DataAnnotations;

namespace HogeschoolPXL.Models
{
    public class AcademieJaar
    {
        public int AcademieJaarId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
    }
}
