namespace Duty2.Models
{
    public class PartOfDay
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Sortpos { get; set; }
        public Group Group { get; set; }
    }
}