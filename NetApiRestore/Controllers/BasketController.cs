using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;

namespace NetApiRestore.Controllers
{
	public class BasketController(StoreContext context) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<BasketDto>> GetBasket()
		{
			//var basket = await context.Baskets
			//	.Include(x => x.Items)
			//	.ThenInclude(x => x.Product)
			//	.FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);

			var basket = await RetrieveBasket();

			if (basket == null) return NoContent();

			return new BasketDto
			{
				BasketId = basket.BasketId,
				Items = basket.Items.Select(x => new BasketItemDto
				{
					ProductId = x.ProductId,
					Name = x.Product.Name,
					Price = x.Product.Price,
					Brand = x.Product.Brand,
					Type = x.Product.Type,
					PictureUrl = x.Product.PictureUrl,
					Quantity = x.Quantity,
				}).ToList()
			};
		}

		[HttpPost]
		public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
		{
			// get bakset 
			var basket = await RetrieveBasket();

			// create basket
			basket ??= CreateBasket();

			// get product 
			var product = await context.Products.FindAsync(productId);
			if (product == null) return BadRequest("Problem adding item to basket");

			// add item to basket 
			basket.AddItem(product, quantity);

			// save changes
			var result = await context.SaveChangesAsync() > 0;

			if (result) return CreatedAtAction(nameof(GetBasket), MapBasketToDto(basket));

			return BadRequest("Problem updating basket");
		}

		private BasketDto MapBasketToDto(Basket basket)
		{
			return new BasketDto
			{
				BasketId = basket.BasketId,
				Items = basket.Items.Select(x => new BasketItemDto
				{
					ProductId = x.ProductId,
					Name = x.Product.Name,
					Price = x.Product.Price,
					Brand = x.Product.Brand,
					Type = x.Product.Type,
					PictureUrl = x.Product.PictureUrl,
					Quantity = x.Quantity,
				}).ToList()
			};
		}

		//[HttpDelete]
		//public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
		//{
		//	return BadRequest("Problem updating basket");
		//}

		private Basket CreateBasket()
		{
			var basketId = Guid.NewGuid().ToString();
			var cookieOptions = new CookieOptions
			{
				IsEssential = true,
				Expires = DateTime.UtcNow.AddDays(30)
			};
			Response.Cookies.Append("basketId", basketId, cookieOptions);
			var basket = new Basket { BasketId = basketId };
			context.Baskets.Add(basket);
			return basket;
		}

		private async Task<Basket?> RetrieveBasket()
		{
			return await context.Baskets
			   .Include(x => x.Items)
			   .ThenInclude(x => x.Product)
			   .FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);
		}
	}
}
