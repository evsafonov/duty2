using System.Data.Entity;

namespace Duty2.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Group> WorkGroups  { get; set; }
        public DbSet<PartOfDay> DayParts { get; set; }
    }
}