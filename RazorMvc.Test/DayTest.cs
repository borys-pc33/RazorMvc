using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using System;
using System.IO;
using System.Reflection;
using WebAPI;
using WebAPI.Controllers;
using Xunit;

namespace RazorMvc.Tests
{
    public class DayTest
    {
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;

            // Act
            DateTime dateTime = DateTimeConvertor.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(2021, dateTime.Year);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(31, dateTime.Day);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            var latitude = 45.75;
            var longitude = 25.3333;
            var APIKey = "35d1f294074ffbb611ac7ee26c783d04";
            var weatherForecastController = CreateWeatherForecastController();

            // Act
            var weatherForcasts = weatherForecastController.Get(latitude, longitude, APIKey);

            // Assert
            // Forecast is volatile so make sure to change the value accordingly
            Assert.Equal(8, weatherForcasts.Count);
        }

        [Fact]
        public void ConvertWeatherApiJsonToWeatherForecast()
        {
            // Assume
            string content = ReadTextFromEmbeddedResource("RazorMvc.Tests.WeatherForecastExample.json");

            var weatherForecastController = CreateWeatherForecastController();

            // Act
            var weatherForcasts = weatherForecastController.ConvertResponseToWeatherForecastList(content);
            var weatherForecastForTommorrow = weatherForcasts[1];

            // Assert
            // Forecast is volatile so make sure to change the value accordingly
            Assert.Equal(285.39, weatherForecastForTommorrow.TemperatureK);
        }

        private string ReadTextFromEmbeddedResource(string resourceName)
        {
            var assembly = this.GetType().Assembly;
            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            using (var tr = new StreamReader(resourceStream))
            {
                return tr.ReadToEnd();
            }
        }

        private static WeatherForecastController CreateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var weatherForecastController = new WeatherForecastController(nullLogger, builder.Build());
            return weatherForecastController;
        }
    }
}
