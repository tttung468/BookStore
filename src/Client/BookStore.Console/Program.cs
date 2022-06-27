// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using BookStore.WebApi.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var baseUrl = "http://localhost:5000";
var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<IWeatherForecastWebApiClient, WeatherForecastWebApiClient>
        (
            c =>
            {
                c.BaseAddress = new Uri(baseUrl);
                c.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                c.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            }
        );
    })
    .Build();

var client = host.Services.GetRequiredService<IWeatherForecastWebApiClient>();
var res = await client.GetWeatherForecastAsync();
foreach (var weatherForecast in res)
{
    Console.WriteLine(weatherForecast.Summary);
}

