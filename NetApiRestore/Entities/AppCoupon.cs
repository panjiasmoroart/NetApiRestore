using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class AppCoupon
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public long? AmountOff { get; set; }

	[Precision(5, 2)]
	public decimal? PercentOff { get; set; }

	public string PromotionCode { get; set; } = string.Empty;
	public required string CouponId { get; set; }
}