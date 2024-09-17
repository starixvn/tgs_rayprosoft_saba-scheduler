using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SabaScheduler.Core.WebClient;
using SabaScheduler.Core.WebClient.MessageHandler;

namespace SabaScheduler.Core.DependencyInjection;

public static class ServiceExtension
{
	public static IServiceCollection AddWebClient(this IServiceCollection service, IConfiguration configuration)
	{
		service.AddRefitClient<IProviderClient>().ConfigureHttpClient(client =>
		{
			client.BaseAddress = new Uri(configuration.GetConnectionString("ProviderUrl")!);
		}).AddHttpMessageHandler<FormUrlEncodedHandler>(); // content-type: x-www-form-urlencoded

		return service;
	}
}