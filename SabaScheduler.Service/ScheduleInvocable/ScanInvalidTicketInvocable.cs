using Coravel.Invocable;
using SabaScheduler.Repository;
using SabaScheduler.Repository.Criteria;

namespace SabaScheduler.Service.ScheduleInvocable;

public class ScanInvalidTicketInvocable(ISentryClient sentryClient, IBetRepository betRepository) : IInvocable
{
	public async Task Invoke()
	{
		var exist = await betRepository.IsInvalidTicketExist(new BetSettleInvalidCriteria());

		if (!exist)
			return;

		_ = sentryClient.CaptureMessage("Invalid ticket running...", SentryLevel.Fatal);
	}
}