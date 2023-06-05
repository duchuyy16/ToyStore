using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ToyStoreAPI.Models;

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

        [HttpPost("GetAllUsers")]
        public List<AspNetUserModel> GetAllUsers()
        {
            var users = _iUsers.GetAll();

            List<AspNetUserModel> lstUser = new List<AspNetUserModel>();

            foreach(var user in users)
            {
                AspNetUserModel userModel = new AspNetUserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                };
                lstUser.Add(userModel);
            }

            return lstUser;
        }

        [HttpPost("GetUserById/{id}")]
        public AspNetUserModel GetUserById(string id)
        {
            var user= _iUsers.GetById(id);

            AspNetUserModel userModel = new AspNetUserModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
            };

            return userModel;
        }

        [HttpPost("CountUsers")]
        public int CountUsers()
        {
            int count = _iUsers.CountUsers();
            return count;
        }
    }
}
