namespace SabaScheduler.Repository;

using Fanex.Data.Repository;
using SabaScheduler.Repository.Criteria;

public class BetRepositoryImp(IDynamicRepository dynamicRepository) : IBetRepository
{
	private const string SuccessSpStatusCode = "0";

	public async Task<bool> IsInvalidTicketExist(ScanBetSettleInvalidCriteria criteria)
	{
		_ = await dynamicRepository.ExecuteAsync(criteria);

		return criteria.Equals(SuccessSpStatusCode);
	}
}