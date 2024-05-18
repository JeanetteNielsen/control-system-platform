using MediatR;
using Microsoft.AspNetCore.Mvc;
using ControlSystemPlatform.BLL.WeatherForcasts.Handlers;
using ControlSystemPlatform.BLL.WeatherForcasts.Model;

namespace ControlSystemPlatform.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(
        IMediator mediator,
        ILogger<WeatherForecastController> logger) : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<List<WeatherForecast>>> Get(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetWeatherForcastsRequest(), cancellationToken);
            return Ok(result);
        }
    }
}