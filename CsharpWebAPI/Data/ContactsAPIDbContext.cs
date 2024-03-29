﻿using CsharpWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CsharpWebAPI.Data;

public class ContactsApiDbContext : DbContext
{
	public ContactsApiDbContext(
		DbContextOptions options) : base(options)
	{ 
	}
	public DbSet<Contact>? Contacts { get; set; }
}