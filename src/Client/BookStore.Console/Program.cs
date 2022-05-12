// See https://aka.ms/new-console-template for more information

using BookStore.WebApi.Client;



Console.WriteLine("Hello, World!");
var client = new WeatherForecastWebApiClient(new WebApiClientConfiguration("http://localhost:44306"));
var res = client.GetAsync().GetAwaiter().GetResult();
foreach (var weatherForecast in res)
{
    Console.WriteLine(weatherForecast.Summary);
}
