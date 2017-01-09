using System;
using System.Collections.Generic;

namespace Duty2.DTOs
{
    public class SelectorContainer
    {
        public string Id { get; set; }
        public List<Selector> Selector { get; set; }
        public int DayPart { get; set; }
        public DateTime Date { get; set; }
    }
}