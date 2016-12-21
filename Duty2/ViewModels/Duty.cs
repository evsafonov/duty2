namespace Duty2.ViewModels
{
    public class Duty
    {
        public int Id { get; set; }
        public Models.User User { get; set; }
        public int Day { get; set; }
        public Models.PartOfDay PartOfDay { get; set; }
    }
}