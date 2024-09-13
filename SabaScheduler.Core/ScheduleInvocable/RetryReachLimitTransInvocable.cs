using Coravel.Invocable;
using Microsoft.Extensions.Options;
using SabaScheduler.Core.Models.ReachLimitTrans;
using SabaScheduler.Core.WebClient;

namespace SabaScheduler.Core.ScheduleInvocable;

internal class RetryReachLimitTransInvocable(IOptions<AppSettings> appSettings, IProviderClient sabaClient) : IInvocable
{
	private AppSettings AppSettings = appSettings.Value;

	public async Task Invoke()
	{
		var result = await sabaClient.GetReachLimitTrans(new ReachLimitTransRequest()
		{
			StartTime = DateTimeOffset.UtcNow.AddDays(-29).DateTime.ToShortDateString(),
			VendorId = AppSettings.VendorId
		});

		if (result.Data?.Txns is null)
		{
			return;
		}

		var groupedByAction = result.Data.Txns
		.SelectMany(txn => txn.TransHistory.Select(history => new { txn, history }))
		.GroupBy(x => x.history.Action)
		.Select(g => new
		{
			Action = g.Key,
			Txns = g.Select(x => x.txn).ToList()
		});

		// Output the grouped data
		foreach (var group in groupedByAction)
		{
			Console.WriteLine($"Action: {group.Action}, count: {group.Txns.Count}");
			var Txns = group.Txns;
			foreach (var txn in group.Txns)
			{
				Console.WriteLine($"  TxId: {txn.TxId}, Stake: {txn.Stake}, UserId: {txn.UserId}");
			}
		}
	}
}