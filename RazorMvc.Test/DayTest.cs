﻿using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using System;
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
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            // Act
            var weatherForcasts = weatherForecastController.FetchWeatherForecasts(latitude, longitude, APIKey);
            WeatherForecast weatherForecastForTommorrow = weatherForcasts[1];

            // Assert
            // Forecast is volatile so make sure to change the value accordingly
            Assert.Equal(285.39, weatherForecastForTommorrow.TemperatureK);
        }
    }
}