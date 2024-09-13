using System.Text.Json.Serialization;

namespace SabaScheduler.Core.Models.ReachLimitTrans;

public record ReachLimitTransRequest
{
	[JsonPropertyName("vendor_id")]
	public required string VendorId { get; set; }

	[JsonPropertyName("start_time")]
	public required string StartTime { get; set; }
}