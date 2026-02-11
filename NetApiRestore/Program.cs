using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
	opt.UseMySql(
		connectionString,
		ServerVersion.AutoDetect(connectionString) 
	);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

// seed the database
DbInitializer.InitDb(app);

app.Run();
