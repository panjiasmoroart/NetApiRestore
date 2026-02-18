using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Entities;

namespace NetApiRestore.Controllers
{
	public class BasketController(StoreContext context) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<Basket>> GetBasket()
		{
			var basket = await context.Baskets
				.Include(x => x.Items)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);

			if (basket == null) return NoContent();

			return basket;
		}

		[HttpPost]
		public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
		{
			// get bakset 
			// create basket   
			// get product 
			// add item to basket 
			// save changes

			return StatusCode(201);
		}

		[HttpDelete]
	}
}
