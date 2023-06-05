namespace ToyStoreAPI.Models
{
    public class AspNetUserModel
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
