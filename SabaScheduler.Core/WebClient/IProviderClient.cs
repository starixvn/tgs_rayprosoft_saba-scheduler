using Refit;
using SabaScheduler.Core.Models.ReachLimitTrans;

namespace SabaScheduler.Core.WebClient;

public interface IProviderClient
{
	[Post("/api/getreachlimittrans")]
	public Task<ReachLimitTransResult> GetReachLimitTrans(ReachLimitTransRequest request);
}