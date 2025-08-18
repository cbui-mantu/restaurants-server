using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

public class TemperatureRange
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class HelloRequest
{
    public string Name { get; set; }
}

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly IWeatherForecastService _weatherForecastService = new WeatherForecastService();
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    [Route("weatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var res = _weatherForecastService.Get();
        return res;
    }

    [HttpGet]
    [Route("actionForecast")]
    public IEnumerable<WeatherForecast> GetAction([FromQuery] int num, [FromBody] TemperatureRange range)
    {
        var res = _weatherForecastService.GetAction(num, range.Min, range.Max);
        return res;
    }

    [HttpGet]
    [Route("healthCheck")]
    public IActionResult HealthCheck()
    {
        var res = _weatherForecastService.HealthCheck();
        return res;
    }

    [HttpPost]
    [Route("generate")]
    public IActionResult Generate([FromQuery] int num, [FromBody] TemperatureRange range)
    {
        if(num < 0 || range.Max < range.Min)
        {
            return BadRequest("Invalid input parameters. Ensure num is non-negative and range is valid.");
        }
        try
        {
            var res = _weatherForecastService.GetAction(num, range.Min, range.Max);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating weather forecast");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    [Route("{lang}/weatherForecast")]
    // https: //localhost:7180/weatherforecast / vn /weatherForecast ? city=London
    public IActionResult GetCity([FromQuery]string city, [FromRoute]string lang)
    {
        var res = _weatherForecastService.HealthCheck();
        return res;
    }

    [HttpPost]
    [Route("hello")]
    // https: //localhost:7180/weatherforecast / hello
    public string Hello( [FromBody] HelloRequest req)
    {
        return $"hello {req.Name}";
    }
}
