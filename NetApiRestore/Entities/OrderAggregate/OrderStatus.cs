namespace NetApiRestore.Entities.OrderAggregate
{
	public enum OrderStatus
	{
		Pending,
		PaymentReceived,
		PaymentFailed,
		PaymentMismatch
	}
}
