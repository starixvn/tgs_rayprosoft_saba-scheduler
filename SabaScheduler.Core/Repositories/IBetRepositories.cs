using SabaScheduler.Core.Criteria;

namespace SabaScheduler.Core.Repositories;

public interface IBetRepository
{
	Task<bool> IsInvalidTicketExist(BetSettleInvalidCriteria criteria);
};