
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        IEnumerable<WeatherForecast> GetAction(int num, int minTemp, int maxTemp);
        IActionResult HealthCheck();
    }
}