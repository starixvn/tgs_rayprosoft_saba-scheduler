namespace SabaScheduler.Core.Models.ReachLimitTrans;

public record ReachLimitTransData
{
	public List<ReachLimitTransTnxs>? Txns { get; set; }
}

public record ReachLimitTransTnxs
{
	public int BetType { get; set; }

	public string? UserId { get; set; }

	public string? LicenseeTxId { get; set; }

	public DateTime WinLostDate { get; set; }

	public long TxId { get; set; }

	public string? RefId { get; set; }

	public decimal? Stake { get; set; }

	public decimal? Odds { get; set; }

	public required List<ReachLimitTransHistory> TransHistory { get; set; }
}

public class ReachLimitTransHistory
{
	public string? Action { get; set; }

	public string? ActionDate { get; set; }

	public decimal Stake { get; set; }

	public decimal Odds { get; set; }

	public short Status { get; set; }

	public decimal? WinLostAmount { get; set; }

	public DateTime? WinLostDate { get; set; }

	public string? OperationId { get; set; }

	public decimal CreditAmount { get; set; }

	public decimal DebitAmount { get; set; }

	public bool InRetry { get; set; }

	public bool ReachRetryLimit { get; set; }
}