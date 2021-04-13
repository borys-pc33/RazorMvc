using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RazorMvc.Utilities;
using RestSharp;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureK = rng.Next(250, 320),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }

        [HttpGet("/forecast")]
        public IList<WeatherForecast> FetchWeatherForecasts(double latitude, double longitude, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return ConvertResponseToWeatherForecastList(response.Content);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IList<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            IList<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                WeatherForecast obj = new WeatherForecast();
                obj.Date = DateTimeConvertor.ConvertEpochToDateTime(item.Value<long>("dt"));
                obj.TemperatureK = item.SelectToken("temp").Value<double>("day");
                obj.Summary = item.SelectToken("weather")[0].Value<string>("main");

                try
                {
                    weatherForecasts.Add(obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return weatherForecasts;
        }
    }
}
