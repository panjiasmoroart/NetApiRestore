using NetApiRestore.Entities;

namespace NetApiRestore.Extensions
{
	public static class ProductExtensions
	{
		public static IQueryable<Product> Sort(this IQueryable<Product> query, string? orderBy)
		{
			query = orderBy switch
			{
				"price" => query.OrderBy(x => x.Price),
				"priceDesc" => query.OrderByDescending(x => x.Price),
				_ => query.OrderBy(x => x.Name)
			};

			return query;
		}
	}
}
