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
		private readonly ContactsAPIDbContext _dbContext;

		public ContactsController(
			ContactsAPIDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetContacts()
		{
			return Ok(await _dbContext.Contacts.ToListAsync());
		}

		[HttpPost]
		public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
		{
			var contact = new Contact()
			{
				Id = new Guid(),
				Name = addContactRequest.Name
			};

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
			var contact = await _dbContext.Contacts.FindAsync(id);
			if (contact == null)
			{
				return NotFound();
			}

			var deleteId =
				_dbContext.Contacts.Remove(contact);

			await _dbContext.SaveChangesAsync();

			return Ok(contact);
		}
	}
}