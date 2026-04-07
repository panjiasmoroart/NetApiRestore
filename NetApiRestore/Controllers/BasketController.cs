using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;
using NetApiRestore.Extensions;
using NetApiRestore.Services;
using MyDiscountService = NetApiRestore.Services.DiscountService;
using Stripe;

namespace NetApiRestore.Controllers
{
	public class BasketController(StoreContext context, MyDiscountService couponService, PaymentsService paymentsService) : BaseApiController
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

			return basket.ToDto();
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

			if (result) return CreatedAtAction(nameof(GetBasket), basket.ToDto());

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

		[HttpDelete]
		public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
		{
			// get basket 
			var basket = await RetrieveBasket();

			if (basket == null) BadRequest("Unable to retrieve basket");

			// remove the item or reduce its quantity 
			basket.RemoveItem(productId, quantity);

			// save changes
			var result = await context.SaveChangesAsync() > 0;

			if (result) return Ok();

			return BadRequest("Problem updating basket");
		}

		[HttpPost("{code}")]
		public async Task<ActionResult<BasketDto>> AddCouponCode(string code)
		{
			// get the basket
			var basket = await RetrieveBasket();
			if (basket == null || string.IsNullOrEmpty(basket.ClientSecret)) return BadRequest("Unable to apply voucher");

			// get the coupon
			var coupon = await couponService.GetCouponFromPromoCode(code);
			if (coupon == null) return BadRequest("Invalid coupon");

			// update the basket with the coupon
			basket.Coupon = coupon;

			// update the payment intent
			var intent = await paymentsService.CreateOrUpdatePaymentIntent(basket);
			if (intent == null) return BadRequest("Problem applying coupon to basket");

			// save changes and return BasketDto if successful
			var result = await context.SaveChangesAsync() > 0;

			if (result) return CreatedAtAction(nameof(GetBasket), basket.ToDto());

			return BadRequest("Problem updating basket");
		}

		[HttpDelete("remove-coupon")]
		public async Task<ActionResult> RemoveCouponFromBasket()
		{
			// get the basket
			var basket = await RetrieveBasket();
			if (basket == null || basket.Coupon == null || string.IsNullOrEmpty(basket.ClientSecret))
				return BadRequest("Unable to update basket with coupon");

			var intent = await paymentsService.CreateOrUpdatePaymentIntent(basket, true);
			if (intent == null) return BadRequest("Problem removing coupon from basket");

			basket.Coupon = null;

			var result = await context.SaveChangesAsync() > 0;

			if (result) return Ok();

			return BadRequest("Problem updating basket");
		}

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
