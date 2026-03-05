using System.Security.Claims;

namespace NetApiRestore.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetUsername(this ClaimsPrincipal user)
		{
			return user.Identity?.Name ?? throw new UnauthorizedAccessException();
		}
	}
}
