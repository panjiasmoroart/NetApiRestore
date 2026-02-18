using Microsoft.EntityFrameworkCore;
using NetApiRestore.Entities;

namespace NetApiRestore.Data
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
	{
		public required DbSet<Product> Products { get; set; }
		public required DbSet<Basket> Baskets { get; set; }
	}
}
