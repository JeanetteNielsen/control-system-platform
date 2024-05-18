using ControlSystemPlatform.DAL.Enities;

namespace ControlSystemPlatform.BLL.TransportOrderDomain
{
    public interface ITransportOrderPublisher
    {
        Task PublishTransportOrderCreatedEvent(TransportOrderEntity entity);
    }

    public class TransportOrderPublisher : ITransportOrderPublisher
    {
        public async Task PublishTransportOrderCreatedEvent(TransportOrderEntity entity)
        {
            // TODO: setup messagebus fx RabbitMQ and a broker fx: Masstransit. Decide whether to go with Choreography or Orchestration. Next steps plan route and schedule execution
        }
    }
}