using Fanex.Data.Repository;
using SabaScheduler.Repository.Helpers;

namespace SabaScheduler.Repository.Criteria.ScanBetSettleInvalid;

public class ScanBetSettleInvalidCriteria : CriteriaBase
{
	public long? Txid { get; set; }

	public string? RefId { get; set; }

	public string? OperationId { get; set; }

	public string? StatusCode { get; set; }

	public string? ErrMsg { get; set; }

	public override string GetSettingKey() => StoreProcedureMapper.BetSettleInvalidSP;

	public override bool IsValid() => true;
}