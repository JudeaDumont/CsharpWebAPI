using CsharpWebAPI.Data;
using Microsoft.EntityFrameworkCore;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
const string reactLocalDefault = "http://localhost:3000";
const string angularLocalDefault = "http://localhost:4200";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: myAllowSpecificOrigins,
		policy =>
		{
			policy.WithOrigins(reactLocalDefault,
				angularLocalDefault)
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		});
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext
	<ContactsAPIDbContext>(
	options => {
		options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString"));

	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
