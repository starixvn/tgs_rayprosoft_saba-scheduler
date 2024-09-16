namespace SabaScheduler.Repository;

using Fanex.Data.Repository;
using SabaScheduler.Core.Criteria;
using SabaScheduler.Core.Repositories;

public class BetRepositoryImp : IBetRepository
{
	private readonly IDynamicRepository dynamicRepository;

	public BetRepositoryImp(IDynamicRepository dynamicRepository)
	{
		this.dynamicRepository = dynamicRepository;
	}

	public async Task<bool> IsInvalidTicketExist(BetSettleInvalidCriteria criteria)
	{
		_ = await dynamicRepository.GetAsync<object>(criteria);
		return criteria.SpResult == 1;
	}
}