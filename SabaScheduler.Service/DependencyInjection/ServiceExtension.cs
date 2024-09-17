using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabaScheduler.Core.Configuration;
using SabaScheduler.Core.WebClient.MessageHandler;
using SabaScheduler.Service.ScheduleInvocable;

namespace SabaScheduler.Service.DependencyInjection;

public static class ServiceExtension
{
	public static IServiceCollection AddScheduleService(this IServiceCollection service)
	{
		service.AddTransient<ReachLimitTransInvocable>();
		service.AddTransient<ScanInvalidTicketInvocable>();
		service.AddTransient<FormUrlEncodedHandler>();

		return service;
	}

	public static void UseCoreScheduler(this IServiceProvider app, IConfiguration configuration)
	{
		var policySettings = configuration.GetSection("PolicySettings").Get<PolicySettings>();

		app.UseScheduler(scheduler =>
		{
			scheduler.Schedule<ReachLimitTransInvocable>().Cron(policySettings!.ReachLimitTransCronTime);
			scheduler.Schedule<ScanInvalidTicketInvocable>().Cron(policySettings!.ScanInvalidTicketCronTime);
		});
	}
}