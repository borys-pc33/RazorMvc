using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly double latitude;
        private readonly double longitude;
        private readonly string apiKey;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            var strLatitude = Environment.GetEnvironmentVariable("WEATHER_FORECAST_LATITUDE") ?? configuration["WeatherForecast:Latitude"];
            latitude = double.Parse(strLatitude, CultureInfo.InvariantCulture);
            var strLongitude = Environment.GetEnvironmentVariable("WEATHER_FORECAST_LONGITUDE") ?? configuration["WeatherForecast:Longitude"];
            longitude = double.Parse(strLongitude, CultureInfo.InvariantCulture);
            apiKey = configuration["WeatherForecast:APIKey"];
            this.logger = logger;
        }

        [HttpGet]
        public List<WeatherForecast> Get()
        {
            List<WeatherForecast> weahterForecasts = Get(latitude, longitude, apiKey);

            return weahterForecasts.GetRange(1, 5);
        }

        [HttpGet("/forecast")]
        public List<WeatherForecast> Get(double latitude, double longitude, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return ConvertResponseToWeatherForecastList(response.Content);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public List<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                try
                {
                    WeatherForecast obj = new WeatherForecast();
                    obj.Date = DateTimeConvertor.ConvertEpochToDateTime(item.Value<long>("dt"));
                    obj.TemperatureK = item.SelectToken("temp").Value<double>("day");
                    obj.Summary = item.SelectToken("weather")[0].Value<string>("main");

                    weatherForecasts.Add(obj);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Cannot get parse weather forecast.");
                }
            }

            return weatherForecasts;
        }
    }
}
