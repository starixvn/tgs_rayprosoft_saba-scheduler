using System.Net;
using System.Text.Json;

namespace SabaScheduler.Middlewares;

public class ExceptionMiddleware(IConfiguration configuration)
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			SentrySdk.CaptureMessage($"{configuration.GetValue<string>("Sentry:Tag")}: {ex.Message} \n {ex.StackTrace}", scope =>
			{
				scope.Level = SentryLevel.Error;
			});
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			var errorResponse = new
			{
				context.Response.StatusCode,
				ex.Message
			};

			await JsonSerializer.SerializeAsync(context.Response.Body, errorResponse).ConfigureAwait(false);
		}
	}
}