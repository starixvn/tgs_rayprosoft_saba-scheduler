using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SabaScheduler.Core.ScheduleInvocable;
using SabaScheduler.Core.WebClient;
using SabaScheduler.Core.WebClient.MessageHandler;

namespace SabaScheduler.Core.DependencyInjection;

public static class ServiceExtension
{
	public static IServiceCollection AddCoreService(this IServiceCollection service)
	{
		service.AddTransient<ReachLimitTransInvocable>();
		service.AddTransient<ProviderHandler>();
		return service;
	}

	public static void UseCoreScheduler(this IServiceProvider app)
	{
		app.UseScheduler(scheduler =>
		{
			scheduler.Schedule<ReachLimitTransInvocable>().Hourly();
		});
	}

	public static IServiceCollection AddWebClient(this IServiceCollection service, IConfiguration configuration)
	{
		service.AddRefitClient<IProviderClient>().ConfigureHttpClient(client =>
		{
			client.BaseAddress = new Uri(configuration.GetConnectionString("ProviderUrl")!);
		}).AddHttpMessageHandler<ProviderHandler>(); // content-type: x-www-form-urlencoded

		return service;
	}
}