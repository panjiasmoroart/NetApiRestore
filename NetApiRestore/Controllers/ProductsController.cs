//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Entities;
using NetApiRestore.Extensions;

namespace NetApiRestore.Controllers
{
    public class ProductsController(StoreContext context) : BaseApiController
	{
        [HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts(string? orderBy, string? searchTerm)
		{
			//var query = context.Products.AsQueryable();

			//query = orderBy switch
			//{
			//	"price" => query.OrderBy(x => x.Price),
			//	"priceDesc" => query.OrderByDescending(x => x.Price),
			//	_ => query.OrderBy(x => x.Name)
			//};

			// custom with extensions
			var query = context.Products
				.Sort(orderBy)
				.Search(searchTerm)
				.AsQueryable();

			return await query.ToListAsync();
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
