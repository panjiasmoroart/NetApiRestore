using System.ComponentModel.DataAnnotations.Schema;

namespace NetApiRestore.Entities
{
	[Table("BasketItems")]
	public class BasketItem
	{
		public int Id { get; set; }
		public int Quantity { get; set; }

		// navigation properties
		public int ProductId { get; set; }
		public required Product Product { get; set; }
		public Basket Basket { get; set; } = null!;
	}
}
