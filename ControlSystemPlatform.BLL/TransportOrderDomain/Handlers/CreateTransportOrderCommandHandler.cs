using ControlSystemPlatform.BLL.Abstractions;
using ControlSystemPlatform.DAL.Command;
using ControlSystemPlatform.DAL.Enities;
using ControlSystemPlatform.DAL.Query;
using MediatR;

namespace ControlSystemPlatform.BLL.TransportOrderDomain.Handlers
{
    public class CreateTransportOrderCommand(CreateTransportOrderCommand.CreateRequest request) : IRequest<Result>
    {
        public CreateRequest Request { get; } = request;

        public record CreateRequest(
            Guid OrderId,
            Guid SourceLocationId,
            Guid DestinationLocationId,
            List<OrderItemDto> Items,
            PriorityLevel Priority,
            DateTime RequiredCompletionTime);

        public record OrderItemDto(string SKU, int Quantity);
    }


    public class CreateTransportOrderCommandHandler(
        IGetOrderItemIdsQuery getOrderItemIdsQuery,
        IGetTransportOrderOrDefault getTransportOrderOrDefault,
        IAddNewTransportOrderCommand addNewTransportOrderCommand,
        ITransportOrderPublisher transportOrderPublisher)
        : IRequestHandler<CreateTransportOrderCommand, Result>
    {
        public async Task<Result> Handle(CreateTransportOrderCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            // TODO: Validate locations exists. The validation Ids, should be looked up, and replaced with internal ones, as done with items.
            var itemSkus = request.Items.Select(x => x.SKU).ToList();
            var itemIds = await getOrderItemIdsQuery.Execute(itemSkus, cancellationToken);

            if (itemSkus.Any(x => !itemIds.ContainsKey(x)))
            {
                return Result.Failure(TransportOrderErrors.InvalidSkuOnOrderItem);
            }
            // TODO: Validate stock, and take RequiredCompletionTime into account. If this is far ahead, and we restock right away, it might cause issues.
            // TODO: Reserve items. And release items if TransportOrderEntity or later checks fail.

            var existingOrder = await getTransportOrderOrDefault.Execute(request.OrderId);
            if (existingOrder != null)
            {
                return Result.Failure(TransportOrderErrors.DuplicatedOrder);
            }

            var entity = new TransportOrderEntity
            {
                RequesterOrderReference = request.OrderId,
                Priority = request.Priority,
                DestinationLocationId = request.DestinationLocationId,
                SourceLocationId = request.SourceLocationId,
                RequiredCompletionTime = request.RequiredCompletionTime,
                Items = request.Items
                    .Select(x => new OrderItemEntity { ItemId = itemIds[x.SKU], Quantity = x.Quantity }).ToList()
            };

            await addNewTransportOrderCommand.Execute(entity);
            await transportOrderPublisher.PublishTransportOrderCreatedEvent(entity);

            return Result.Success();
        }
    }
}