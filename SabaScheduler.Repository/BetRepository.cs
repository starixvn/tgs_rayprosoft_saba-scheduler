namespace SabaScheduler.Repository;

using Fanex.Data.Repository;
using SabaScheduler.Repository.Criteria;

public class BetRepositoryImp(IDynamicRepository dynamicRepository) : IBetRepository
{
	public async Task<bool> IsInvalidTicketExist(BetSettleInvalidCriteria criteria)
	{
		var result = await dynamicRepository.GetAsync<int>(criteria);

		// TODO: Remove Magic number
		return result == 1;
	}
}