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
    public class Statuses : BaseRepository<Status>, IStatus
    {
        public Statuses(ToyStoreContext context) : base(context)
        {
        }

        public Status Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Status> GetAll()
        {
            return _context.Statuses.ToList();
        }

        public Status GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsIdExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}
