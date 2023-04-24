namespace ToyStoreAPI.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public List<UserModel> Users { get; set; } = null!;
    }
}
