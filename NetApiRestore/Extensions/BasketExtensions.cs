using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;

namespace NetApiRestore.Extensions
{
	public static class BasketExtensions
	{
		public static BasketDto ToDto(this Basket basket)
		{
			return new BasketDto
			{
				BasketId = basket.BasketId,
				ClientSecret = basket.ClientSecret,
				//PaymentItentId = basket.PaymentIntentId,
				Coupon = basket.Coupon,
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

		public static async Task<Basket> GetBasketWithItems(this IQueryable<Basket> query, string? basketId)
		{
			return await query
				.Include(x => x.Items)
				.ThenInclude(x => x.Product)
				.Include(x => x.Coupon)
				.FirstOrDefaultAsync(x => x.BasketId == basketId)
					?? throw new Exception("Cannot get basket");
		}
	}

}