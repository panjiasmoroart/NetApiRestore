using NetApiRestore.Entities;
using Stripe;

namespace NetApiRestore.Services
{
	public class PaymentsService(IConfiguration config)
	{
		public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Basket basket)
		{
			StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

			var service = new PaymentIntentService();

			var intent = new PaymentIntent();

			long subtotal = basket.Items.Sum(x => x.Quantity * x.Product.Price);

			long deliveryFee = subtotal > 10000 ? 0 : 500;

			// long discount = 0;

			// logic discount

			var totalAmount = subtotal  + deliveryFee;

			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = totalAmount,
					Currency = "usd",
					PaymentMethodTypes = ["card"]
				};
				intent = await service.CreateAsync(options);
			}
			else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = totalAmount
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			return intent;
		}
	}
}
