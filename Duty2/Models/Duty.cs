using System;

namespace Duty2.Models
{
    public class Duty
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; } 
        public PartOfDay PartOfDay { get; set; }
    }
}