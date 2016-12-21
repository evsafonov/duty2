namespace Duty2.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool Isadmin { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
    }
}