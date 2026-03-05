using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Entities;
using NetApiRestore.Entities.OrderAggregate;

namespace NetApiRestore.Data
{
    public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
	{
		public required DbSet<Product> Products { get; set; }
		public required DbSet<Basket> Baskets { get; set; }
		public required DbSet<Order> Orders { get; set; }   

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityRole>()
				.HasData(
					new IdentityRole { Id = "1abc9de4-45e1-4900-9f79-be8db3f90e3c", Name = "Member", NormalizedName = "MEMBER" },
					new IdentityRole { Id = "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9", Name = "Admin", NormalizedName = "ADMIN" }
				);
		}
	}
}
