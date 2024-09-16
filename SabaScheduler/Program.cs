using Coravel;
using Microsoft.AspNetCore.HttpLogging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SabaScheduler.Core;
using SabaScheduler.Core.DependencyInjection;
using SabaScheduler.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseSentry();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services
	.AddOptions<AppSettings>()
	.Configure((AppSettings options, IServiceProvider sp) =>
	{
		var configuration = sp.GetRequiredService<IConfiguration>();
		configuration.GetSection("AppSettings").Bind(options);
	})
	.Services
	.AddScheduler()
	.AddCoreService();

builder.Services
	.AddHttpLogging(logging => logging.LoggingFields = HttpLoggingFields.All)
	.AddHealthChecks();

builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.AddWebClient(builder.Configuration);

var excludeExtensions = new[] { "html", "ico", "png", "jpg" };

builder.Services
	.AddOpenTelemetry()
	.ConfigureResource(_ => _.AddService(builder.Configuration.GetValue<string>("Otel:ServiceName") ?? ""))
	.WithTracing(tracerProviderBuilder => tracerProviderBuilder
		.AddSource("Tgs.*")
		.AddSource("TGS.*")
		.AddSource("Valhalla.*")
		.AddAspNetCoreInstrumentation(options => options.Filter =
			httpContext => !httpContext.Request.Path.HasValue
				&& !httpContext.Request.Path.StartsWithSegments("/health")
				&& !httpContext.Request.Path.StartsWithSegments("/healthcheck")
				&& !httpContext.Request.Path.StartsWithSegments("/profiler/")
				&& !httpContext.Request.Path.StartsWithSegments("/metrics")
				&& !excludeExtensions.Any(
					ext => httpContext.Request.Path.Value!.EndsWith($".{ext}")))
		.AddHttpClientInstrumentation()
		.SetSampler(new AlwaysOnSampler())
		.AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
		.AddOtlpExporter(options => options.Endpoint = builder.Configuration.GetValue<Uri>("Otel:Collector") ?? new Uri("")))
	.WithMetrics(meterBuiler => meterBuiler
		.SetResourceBuilder(ResourceBuilder.CreateDefault()
			.AddEnvironmentVariableDetector())
			.AddRuntimeInstrumentation()
			.AddAspNetCoreInstrumentation()
			.AddOtlpExporter())
	.Services
	.AddProfileViewer()
	.AddInterceptionService()
	.AddServiceUtiltiyInterceptors();

builder.Services.AddRequestDecompression();

var app = builder.Build();

app.UseRequestDecompression();

app.MapHealthChecks("/health");
app.MapHealthChecks("/healthcheck");
app.MapHealthChecks("/");

app.Services.UseCoreScheduler(builder.Configuration);

app.Run();