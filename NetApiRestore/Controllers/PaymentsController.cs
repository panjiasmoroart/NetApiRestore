using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApiRestore.Data;
using NetApiRestore.DTOs;
using NetApiRestore.Extensions;
using NetApiRestore.Services;
using Stripe;

namespace NetApiRestore.Controllers
{
	public class PaymentsController(
		PaymentsService paymentsService, 
		StoreContext context, IConfiguration config, 
		ILogger<PaymentsController> logger
	) : BaseApiController
	{
		[Authorize]
		[HttpPost]
		public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
		{
			var basket = await context.Baskets.GetBasketWithItems(Request.Cookies["basketId"]);

			if (basket == null) return BadRequest("Problem with the basket");

			var intent = await paymentsService.CreateOrUpdatePaymentIntent(basket);

			if (intent == null) return BadRequest("Problem creating payment intent");

			basket.PaymentIntentId ??= intent.Id;
			basket.ClientSecret ??= intent.ClientSecret;

			if (context.ChangeTracker.HasChanges())
			{
				var result = await context.SaveChangesAsync() > 0;

				if (!result) return BadRequest("Problem updating basket with intent");
			}

			return basket.ToDto();
		}

		[HttpPost("webhook")]
		public async Task<IActionResult> StripeWebhook()
		{
			var json = await new StreamReader(Request.Body).ReadToEndAsync();

			try 
			{
				var stripeEvent = ConstructStripeEvent(json);
				if (stripeEvent.Data.Object is not PaymentIntent intent)
				{
					return BadRequest("Invalid event data");
				}

				if (intent.Status == "succeeded") await HandlePaymentIntentSucceeded(intent);
				else await HandlePaymentIntentFailed(intent);

				return Ok();
			}
			catch(StripeException ex)
			{
				logger.LogError(ex, "Stripe webhook error");
				return StatusCode(StatusCodes.Status500InternalServerError, "Webhook error");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An expected error has occurred");
				return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error");
			}
		}

		private async Task HandlePaymentIntentFailed(PaymentIntent intent)
		{
			throw new NotImplementedException();
		}

		private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
		{
			throw new NotImplementedException();
		}
		private Event ConstructStripeEvent(string json)
		{
			try
			{
				return EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], config["StripeSettings:WhSecret"]);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to construct stripe event");
				throw new StripeException("Invalid signature");
			}
		}
	}
}
