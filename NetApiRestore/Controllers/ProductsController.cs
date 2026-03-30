//using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;
using NetApiRestore.Extensions;
using NetApiRestore.RequestHelpers;
using NetApiRestore.Services;

namespace NetApiRestore.Controllers
{
    public class ProductsController(StoreContext context, IMapper mapper, ImageService imageService) : BaseApiController
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

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
		{
			Console.WriteLine("Insert Product");
			var product = mapper.Map<Product>(productDto);

			// jika ada gambar yg diupload
			if (productDto.File != null)
			{
				var imageResult = await imageService.AddImageAsync(productDto.File);
				if (imageResult.Error != null)
				{
					return BadRequest(imageResult.Error.Message);
				}

				product.PictureUrl = imageResult.SecureUrl.AbsoluteUri;
				product.PublicId = imageResult.PublicId;
			}

			context.Products.Add(product);

			var result = await context.SaveChangesAsync() > 0;

			if (result) return CreatedAtAction(nameof(GetProduct), new { Id = product.Id }, product);

			return BadRequest("Problem creating new procuct");
		}

		[Authorize(Roles = "Admin")]
		[HttpPut]
		public async Task<ActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var product = await context.Products.FindAsync(updateProductDto.Id);

			if (product == null) return NotFound();

			mapper.Map(updateProductDto, product);

			var result = await context.SaveChangesAsync() > 0;

			if (result) return NoContent();

			return BadRequest("Problem updating product");
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			var product = await context.Products.FindAsync(id);

			if (product == null) return NotFound();

			context.Products.Remove(product);

			var result = await context.SaveChangesAsync() > 0;

			if (result) return Ok();

			return BadRequest("Problem deleting the product");
		}



	}
}
