using Fanex.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace SabaScheduler.Repository.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	   => services.AddScoped<IDynamicRepository, DynamicRepository>().AddScoped<IBetRepository, BetRepositoryImp>();
}