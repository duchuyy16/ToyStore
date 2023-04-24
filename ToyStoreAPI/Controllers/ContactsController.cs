using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Net;
using System.Net.Mail;
using ToyStoreAPI.Helpers;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContact _iContact;
        public ContactsController(IContact iContact)
        {
            _iContact = iContact;
        }

        [HttpPost("AddContact")]
        public ContactModel AddContact(ContactModel contactModel)
        {
            var newContact = new Contact
            {
                ContactName = contactModel.ContactName,
                Email = contactModel.Email,
                Message = contactModel.Message,
                CreateAt=DateTime.Now 
            };

            var addedContact = _iContact.Add(newContact);

            if (addedContact != null)
            {
                EmailHelper.SendEmail(addedContact.ContactName,addedContact.Email, addedContact.Message);
            }

            return new ContactModel
            {
                ContactName = addedContact.ContactName,
                Email = addedContact.Email,
                Message = addedContact.Message,
                CreateAt = DateTime.Now
            };
        }
    }
}
