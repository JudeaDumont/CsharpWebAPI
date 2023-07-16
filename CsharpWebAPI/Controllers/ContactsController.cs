using CsharpWebAPI.Data;
using CsharpWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CsharpWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsApiDbContext _dbContext;

        public ContactsController(
            ContactsApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            if (_dbContext.Contacts != null) return Ok(await _dbContext.Contacts.ToListAsync());
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = new Guid(),
                Name = addContactRequest.Name
            };

            if (_dbContext.Contacts == null)
            {
                return NotFound();
            }

            await _dbContext.Contacts.AddAsync(contact);

            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult>
            UpdateContact(
                [FromRoute] Guid id,
                UpdateContactRequest updateContactRequest)
        {
            if (_dbContext.Contacts == null) return NotFound();
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            contact.Name = updateContactRequest.Name;
            await _dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult>
            GetContact(
                [FromRoute] Guid id)
        {
            if (_dbContext.Contacts == null) return NotFound();
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>
            DeleteContact(
                [FromRoute] Guid id)
        {
            if (_dbContext.Contacts == null) return NotFound();
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _dbContext.Contacts.Remove(contact);

            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}