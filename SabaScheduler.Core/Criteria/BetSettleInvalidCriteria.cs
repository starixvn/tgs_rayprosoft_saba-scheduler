using Fanex.Data.Repository;
using SabaSingleWallet.Repositories.Helpers;

namespace SabaScheduler.Core.Criteria;

public class BetSettleInvalidCriteria : CriteriaBase
{
	public int SpResult { get; set; } = 0;

	public override string GetSettingKey() => StoreProcedureMapper.BetSettleInvalidSP;

	public override bool IsValid() => true;
}