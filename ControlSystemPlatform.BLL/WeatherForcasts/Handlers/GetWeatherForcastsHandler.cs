using MediatR;
using ControlSystemPlatform.BLL.WeatherForcasts.Mappers;
using ControlSystemPlatform.BLL.WeatherForcasts.Model;
using ControlSystemPlatform.DAL.Query;

namespace ControlSystemPlatform.BLL.WeatherForcasts.Handlers
{
    public class GetWeatherForcastsRequest : IRequest<List<WeatherForecast>>;

    public class GetWeatherForcastsRequestHandler(IGetWeatherForcasts getWeatherForcasts)
        : IRequestHandler<GetWeatherForcastsRequest, List<WeatherForecast>>
    {
        public async Task<List<WeatherForecast>> Handle(GetWeatherForcastsRequest request,
            CancellationToken cancellationToken)
        {
            return (await getWeatherForcasts.Execute(cancellationToken))
                .Select(x => x.MapFromEntity()).ToList();
        }
    }
}