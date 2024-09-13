using System.Text.Json.Serialization;

namespace SabaScheduler.Core.Models.ReachLimitTrans;

public record ReachLimitTransResult
{
	public ReachLimitTransData? Data { get; set; }

	[JsonPropertyName("error_code")]
	public int ErrorCode { get; set; }

	public string? Message { get; set; }
}