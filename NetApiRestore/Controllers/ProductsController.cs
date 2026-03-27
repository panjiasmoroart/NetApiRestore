//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;
using NetApiRestore.Extensions;
using NetApiRestore.RequestHelpers;

namespace NetApiRestore.Controllers
{
    public class ProductsController(StoreContext context) : BaseApiController
	{
        [HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts(
			[FromQuery]ProductParams productParams	
		)
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
				.Sort(productParams.OrderBy)
				.Search(productParams.SearchTerm)
				.Filter(productParams.Brands, productParams.Types)
				.AsQueryable();

			var products = await PagedList<Product>.ToPagedList(
				query,
				productParams.PageNumber,
				productParams.PageSize
			);

			//return await query.ToListAsync();

			//return Ok(new {Items = products, products.Metadata });

			Response.AddPaginationHeader(products.Metadata);

			return products;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await context.Products.FindAsync(id);

			if (product == null) return NotFound();

            return product;
		}

		// https://localhost:5001/api/products/filters
		[HttpGet("filters")]
		public async Task<IActionResult> GetFilters()
		{
			var brands = await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
			var types = await context.Products.Select(x => x.Type).Distinct().ToListAsync();

			return Ok(new { brands, types });
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
		{
			var product = new Product{ Name = productDto.Name };

			context.Products.Add(product);

			var result = await context.SaveChangesAsync() > 0;

			if (result) return CreatedAtAction(nameof(GetProduct), new { Id = product.Id }, product);

			return BadRequest("Problem creating new procuct");
		}

	}
}
