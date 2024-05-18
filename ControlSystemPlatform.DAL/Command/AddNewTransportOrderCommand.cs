using ControlSystemPlatform.DAL.Enities;
using ControlSystemPlatform.Shared;

namespace ControlSystemPlatform.DAL.Command
{
    public interface IAddNewTransportOrderCommand
    {
        Task Execute(TransportOrderEntity entity);
    }

    public class AddNewTransportOrderCommand(WarehouseDbContext context, IScopedContext scopedContext)
        : IAddNewTransportOrderCommand
    {
        public async Task Execute(TransportOrderEntity entity)
        {
            entity.RequestedBy = scopedContext.UserId;
            await context.TransportOrder.AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}