using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 44));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
	opt.UseMySql(connectionString, mysqlServerVersion);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

// seed the database
DbInitializer.InitDb(app);

app.Run();
