using Services.Base;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Function
{
    public class Users : BaseRepository<Users>, IUser
    {
        public Users(ToyStoreContext context) : base(context)
        {
        }

        public AspNetUser? Add(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public int CountUsers()
        {
            return _context.AspNetUsers.Count();
        }

        public bool Delete(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(AspNetUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
