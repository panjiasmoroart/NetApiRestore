using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApiRestore.Data;
using NetApiRestore.Entities.OrderAggregate;
using NetApiRestore.Extensions;

namespace NetApiRestore.Controllers
{
	[Authorize]
	public class OrdersController(StoreContext context) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<List<Order>>> GetOrders()
		{
			var orders = await context.Orders
				.Include(x => x.OrderItems)
				.Where(x => x.BuyerEmail == User.GetUsername())
				.ToListAsync();

			return orders;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Order>> GetOrderDetails(int id)
		{
			var order = await context.Orders
				.Where(x => x.BuyerEmail == User.GetUsername() && id == x.Id)
				.FirstOrDefaultAsync();

			if (order == null) return NotFound();

			return order;
		}
	}
}
