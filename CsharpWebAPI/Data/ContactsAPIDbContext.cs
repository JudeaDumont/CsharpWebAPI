using CsharpWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CsharpWebAPI.Data
{
	public class ContactsAPIDbContext : DbContext
	{

	public ContactsAPIDbContext(
		DbContextOptions options) : base(options)
		{ 
		}
		public DbSet<Contact> Contacts { get; set; }
	}
}
