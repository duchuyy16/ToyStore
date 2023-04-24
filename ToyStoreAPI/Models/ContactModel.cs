namespace ToyStoreAPI.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
