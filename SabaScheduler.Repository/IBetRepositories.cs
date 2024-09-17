using SabaScheduler.Repository.Criteria;

namespace SabaScheduler.Repository;

public interface IBetRepository
{
	Task<bool> IsInvalidTicketExist(ScanBetSettleInvalidCriteria criteria);
};