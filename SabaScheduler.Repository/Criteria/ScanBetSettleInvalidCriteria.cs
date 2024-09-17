using Fanex.Data.Repository;
using SabaScheduler.Repository.Helpers;

namespace SabaScheduler.Repository.Criteria;

public class ScanBetSettleInvalidCriteria : CriteriaBase, INonQueryCommand
{
	public string? StatusCode { get; set; }

	public string? ErrMsg { get; set; }

	public override string GetSettingKey() => StoreProcedureMapper.BetSettleInvalidSP;

	public override bool IsValid() => true;
}