using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Entities;

namespace NetApiRestore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(StoreContext context) : ControllerBase
    {
        [HttpGet]
		public ActionResult<List<Product>> GetProducts()
		{
			return context.Products.ToList();
		}

		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			var product =  context.Products.Find(id);

			if (product == null) return NotFound();

            return product;
		}
	}
}
