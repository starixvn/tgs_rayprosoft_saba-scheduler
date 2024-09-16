using Coravel.Invocable;
using SabaScheduler.Core.Repositories;

namespace SabaScheduler.Core.ScheduleInvocable
{
	public class ScanInvalidTicketInvocable(ISentryClient sentryClient, IBetRepository betRepository) : IInvocable
	{
		public async Task Invoke()
		{
			var exist = await betRepository.IsInvalidTicketExist(new Criteria.BetSettleInvalidCriteria());

			if (!exist)
			{
				return;
			}

			_ = sentryClient.CaptureMessage("Invalid ticket running...", SentryLevel.Fatal);
		}
	}
}