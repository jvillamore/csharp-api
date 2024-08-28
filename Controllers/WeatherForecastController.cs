using Microsoft.AspNetCore.Mvc;

namespace csharp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private static List<WeatherForecast> weatherList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    }).ToList();



    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return weatherList;
    }


    [HttpPost]
    public IActionResult Post(WeatherForecast objWeather)
    {
        weatherList.Add(objWeather);
        return Ok();
    }

    [HttpPut]
    public IActionResult Put(WeatherForecast inWeather)
    {
        var objWeather = weatherList.Find(s => s.Summary == inWeather.Summary);
        if (objWeather is not null)
        {
            weatherList.Remove(objWeather);
            objWeather.Summary = inWeather.Summary;
            objWeather.Date = inWeather.Date;
            objWeather.TemperatureC = inWeather.TemperatureC;
            weatherList.Add(objWeather);
        }
        return Ok();
    }

    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        weatherList.RemoveAt(index);
        return Ok();
    }
}
