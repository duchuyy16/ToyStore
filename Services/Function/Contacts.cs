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
    public class Contacts : BaseRepository<Contact>, IContact
    {
        public Contacts(ToyStoreContext context) : base(context)
        {
        }

        public Contact Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Contact GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsIdExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}
