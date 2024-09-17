using Newtonsoft.Json;

namespace SabaScheduler.Core.WebClient.MessageHandler;

public class FormUrlEncodedHandler : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (request.Content is not null)
		{
			var baseContent = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(baseContent);

			request.Content = new FormUrlEncodedContent(dict!);
		}

		HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
		return response;
	}
}