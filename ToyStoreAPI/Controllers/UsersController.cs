using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _iUsers;
        public UsersController(IUser iUsers)
        {
            _iUsers = iUsers;
        }
        [HttpPost("CountUsers")]
        public int CountUsers()
        {
            int count = _iUsers.CountUsers();
            return count;
        }
    }
}
