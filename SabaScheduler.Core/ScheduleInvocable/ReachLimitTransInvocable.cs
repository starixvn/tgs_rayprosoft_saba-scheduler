using System.Text;
using Coravel.Invocable;
using Microsoft.Extensions.Options;
using SabaScheduler.Core.Models.ReachLimitTrans;
using SabaScheduler.Core.WebClient;

namespace SabaScheduler.Core.ScheduleInvocable;

internal class ReachLimitTransInvocable(IOptions<AppSettings> appSettings, ISentryClient sentryClient, IProviderClient sabaClient) : IInvocable
{
	private AppSettings AppSettings = appSettings.Value;

	public async Task Invoke()
	{
		var result = await sabaClient.GetReachLimitTrans(new ReachLimitTransRequest()
		{
			StartTime = DateTimeOffset.UtcNow.AddDays(-29).DateTime.ToShortDateString(),
			VendorId = AppSettings.VendorId
		});

		LogResult(result.Data?.Txns);
	}

	private void LogResult(List<ReachLimitTransTnxs>? txns)
	{
		if (txns is null)
		{
			return;
		}

		var groupedByAction = txns
		.SelectMany(txn => txn.TransHistory.Select(history => new { txn, history }))
		.GroupBy(x => x.history.Action)
		.Select(g => new
		{
			Action = g.Key,
			Txns = g.Select(x => x.txn).ToList()
		});

		StringBuilder events = new StringBuilder();
		_ = events.AppendLine($"Reach Limit Transaction report at {DateTimeOffset.UtcNow.DateTime} (UTC)");

		foreach (var group in groupedByAction)
		{
			_ = events.AppendLine($"Action: {group.Action}, count: {group.Txns.Count}");
		}

		sentryClient.CaptureMessage(events.ToString(), SentryLevel.Fatal);
	}
}