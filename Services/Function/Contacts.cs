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
            return _context.Contacts.Find(id)!;
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.SingleOrDefault(c => c.ContactId == id)!;
        }

        public bool IsIdExist(int id)
        {
            return _context.Contacts.Any(p => p.ContactId == id);
        }
    }
}
