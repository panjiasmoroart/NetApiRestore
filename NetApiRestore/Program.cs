using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 44)); // Fixed

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
	opt.UseMySql(connectionString, mysqlServerVersion);
});
builder.Services.AddCors();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(opt =>
{
	opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
});

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

// seed the database
DbInitializer.InitDb(app);

app.Run();
