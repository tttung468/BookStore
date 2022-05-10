using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BookStore.WebApi.Client;

public class WebApiClientBase
{
    private readonly WebApiClientConfiguration _configuration;

    public WebApiClientBase(WebApiClientConfiguration configuration)
    {
        this._configuration = configuration;
        this.BaseUrl = configuration.ServerBaseUrl;
    }

    public string BaseUrl { get; set; }

    protected JsonSerializerSettings ApplyCommonJsonSerializerSettings(
        JsonSerializerSettings settings)
    {
        settings.ApplyCommonNewtonsoftJsonSettings();
        return settings;
    }

    protected Task<HttpClient> CreateHttpClientAsync(
        CancellationToken cancellationToken)
    {
        HttpClient result = new HttpClient();
        result.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        result.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        if (this._configuration.Timeout.HasValue)
            result.Timeout = this._configuration.Timeout.Value;
        return Task.FromResult<HttpClient>(result);
    }
}
