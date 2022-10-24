namespace HogeschoolPXL.Models
{
    public class Handboek
    {
        public int HandboekID { get; set; }
        public string Titel { get; set; }
        public decimal KostPrijs { get; set; }
        public DateTime UitgifteDatum { get; set; }
        public string Afbeelding { get; set; }

    }
}
