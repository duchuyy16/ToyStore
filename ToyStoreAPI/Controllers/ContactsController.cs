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
        private readonly EmailHelper _emailHelper;
        public ContactsController(IContact iContact, EmailHelper emailHelper)
        {
            _iContact = iContact;
            _emailHelper = emailHelper;
        }

        [HttpPost("GetAllContacts")]
        public List<ContactModel> GetAllContacts()
        {
            var contacts = _iContact.GetAll();

            List<ContactModel> lstContact = new List<ContactModel>();

            foreach (var item in contacts)
            {
                ContactModel contactModel = new ContactModel
                {
                    ContactId = item.ContactId,
                    ContactName = item.ContactName,
                    Email = item.Email,
                    Message = item.Message,
                    CreateAt = DateTime.Now,
                };
                lstContact.Add(contactModel);
            }

            return lstContact;
        }


        [HttpPost("GetContactById/{id}")]
        public ContactModel GetContactById(int id)
        {
            var contact = _iContact.GetById(id);

            ContactModel contactModel = new ContactModel
            {
                ContactId = contact.ContactId,
                ContactName = contact.ContactName,
                CreateAt = DateTime.Now,
                Email = contact.Email,
                Message = contact.Message,
            };

            return contactModel;
        }

        [HttpPost("FindContactById/{id}")]
        public ContactModel FindContactById(int id)
        {
            var contact = _iContact.Find(id);

            ContactModel contactModel = new ContactModel
            {
                ContactId = contact.ContactId,
                ContactName = contact.ContactName,
                CreateAt = DateTime.Now,
                Email = contact.Email,
                Message = contact.Message,
            };

            return contactModel;
        }

        [HttpPost("ExistsById/{id}")]
        public bool ExistsById(int id)
        {
            return _iContact.IsIdExist(id);
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
                _emailHelper.SendEmail(addedContact.ContactName, addedContact.Email, addedContact.Message);
            }

            return new ContactModel
            {
                ContactName = addedContact!.ContactName,
                Email = addedContact.Email,
                Message = addedContact.Message,
                CreateAt = DateTime.Now
            };
        }

        [HttpPost("UpdateContact")]
        public bool UpdateContact(ContactModel contactModel)
        {
            var contact= new Contact
            {
                ContactId = contactModel.ContactId,
                ContactName = contactModel.ContactName,
                Email = contactModel.Email,
                Message = contactModel.Message,
                CreateAt = DateTime.Now
            };
            var updateResult = _iContact.Update(contact);
            return updateResult;
        }
        
        [HttpPost("DeleteContact")]
        public bool DeleteContact(ContactModel contactModel)
        {
            var contact = _iContact.GetById(contactModel.ContactId);
            if (contact == null) return false;
            var deleteResult = _iContact.Delete(contact);
            return deleteResult;
        }
    }
}
