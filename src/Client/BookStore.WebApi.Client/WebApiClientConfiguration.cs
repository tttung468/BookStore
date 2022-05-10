namespace BookStore.WebApi.Client;

public class WebApiClientConfiguration
{
    public WebApiClientConfiguration(
        string serverBaseUrl,
        TimeSpan? timeout = null)
    {
        this.ServerBaseUrl = serverBaseUrl;
        this.Timeout = timeout;
    }

    public string ServerBaseUrl { get; }

    public TimeSpan? Timeout { get; }
}
