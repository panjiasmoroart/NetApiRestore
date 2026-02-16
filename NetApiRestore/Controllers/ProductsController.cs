//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Entities;

namespace NetApiRestore.Controllers
{
    public class ProductsController(StoreContext context) : BaseApiController
	{
        [HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			return await context.Products.AsNoTracking().ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await context.Products.FindAsync(id);

			if (product == null) return NotFound();

            return product;
		}
	}
}
