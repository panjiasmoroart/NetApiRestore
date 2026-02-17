namespace NetApiRestore.Middleware
{
	public class ExceptionMiddleware(IHostEnvironment env, ILogger<ExceptionMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
		}

		private async Task HandleException(HttpContext context, Exception ex)
		{
			throw new NotImplementedException();
		}
	}
}
