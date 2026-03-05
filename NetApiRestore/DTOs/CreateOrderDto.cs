using NetApiRestore.Entities.OrderAggregate;

namespace NetApiRestore.DTOs
{
	public class CreateOrderDto
	{
		public required ShippingAddress ShippingAddress { get; set; }
		public required PaymentSummary PaymentSummary { get; set; }
	}
}
