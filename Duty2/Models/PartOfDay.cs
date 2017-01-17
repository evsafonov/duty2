namespace Duty2.Models
{
    public class PartOfDay
    {
        public int Id { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public int Sortpos { get; set; }
        public Group Group { get; set; }
    }
}