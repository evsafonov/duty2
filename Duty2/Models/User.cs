namespace Duty2.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool Isadmin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TelegramNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsHidden { get; set; }
        public Group Group { get; set; }
    }
}