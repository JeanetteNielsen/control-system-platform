using ControlSystemPlatform.BLL.WeatherForcasts.Model;
using ControlSystemPlatform.DAL;

namespace ControlSystemPlatform.BLL.WeatherForcasts.Mappers
{
    internal static class WeatherforcastExtension
    {
        public static WeatherForecast MapFromEntity(this WeatherForecastEntity entity)
        {
            return new WeatherForecast
            {
                Date = entity.Date,
                Summary = entity.Summary,
                TemperatureC = (int)entity.TemperatureCelsius,
                Id = entity.Id
            };
        }
    }
}