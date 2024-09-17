using System.Text;
using Coravel.Invocable;
using SabaScheduler.Repository;
using SabaScheduler.Repository.Criteria.ScanBetSettleInvalid;

namespace SabaScheduler.Service.ScheduleInvocable;

public class ScanInvalidTicketInvocable(ISentryClient sentryClient, IBetRepository repository) : IInvocable
{
	public async Task Invoke()
	{
		var result = await repository.ScanBetSettleInvalidTicket(new ScanBetSettleInvalidCriteria());

		if (result is null)
		{
			return;
		}

		var events = new StringBuilder();
		_ = events.AppendLine($"Invalid Ticket Running");
		_ = events.AppendLine($"- Txid: {result.Txid}, OperationId: {result.OperationId}, RefId: {result.RefId}");

		_ = sentryClient.CaptureMessage(events.ToString(), SentryLevel.Fatal);
	}
}