using SabaScheduler.Repository.Criteria.ScanBetSettleInvalid;

namespace SabaScheduler.Repository;

public interface IBetRepository
{
	Task<ScanBetSettleInvalidSPResult?> ScanBetSettleInvalidTicket(ScanBetSettleInvalidCriteria scanBetSettleInvalidCriteria);
};