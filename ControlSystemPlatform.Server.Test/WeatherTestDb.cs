using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ControlSystemPlatform.DAL;
using ControlSystemPlatform.Server.Infrastructure;

namespace ControlSystemPlatform.Server.Test;

public class WeatherTestDb
{
    public WeatherDbContext Context { get; }

    public WeatherTestDb(WeatherDbContext context)
    {
        Context = context;
    }

    public WeatherTestDb WithWeatherForcast(WeatherForecastEntity entity)
    {
        Context.WeatherForecast.Add(entity);
        Context.SaveChanges();
        return this;
    }

    public WeatherTestDb WithWeatherForcasts(List<WeatherForecastEntity> entities)
    {
        Context.WeatherForecast.AddRange(entities);
        Context.SaveChanges();
        return this;
    }
}