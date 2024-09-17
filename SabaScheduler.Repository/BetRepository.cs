namespace SabaScheduler.Repository;

using Fanex.Data.Repository;
using SabaScheduler.Repository.Criteria.ScanBetSettleInvalid;

public class BetRepositoryImp(IDynamicRepository dynamicRepository) : IBetRepository
{
	private const string SuccessSpStatusCode = "0";

	public async Task<ScanBetSettleInvalidSPResult?> ScanBetSettleInvalidTicket(ScanBetSettleInvalidCriteria scanBetSettleInvalidCriteria)
	{
		var result = await dynamicRepository.GetAsync<ScanBetSettleInvalidSPResult>(scanBetSettleInvalidCriteria);
		if (scanBetSettleInvalidCriteria.StatusCode!.Equals(SuccessSpStatusCode))
		{
			return null;
		}

		return result;
	}
}