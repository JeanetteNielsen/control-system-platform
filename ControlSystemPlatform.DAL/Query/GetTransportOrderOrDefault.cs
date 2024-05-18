using ControlSystemPlatform.DAL.Enities;
using Microsoft.EntityFrameworkCore;

namespace ControlSystemPlatform.DAL.Query;

public interface IGetTransportOrderOrDefault
{
    Task<TransportOrderEntity> Execute(Guid requesterOrderReference);
}

public class GetTransportOrderOrDefault(WarehouseDbContext context) : IGetTransportOrderOrDefault
{
    public async Task<TransportOrderEntity?> Execute(Guid requesterOrderReference)
    {
        return await context.TransportOrder.SingleOrDefaultAsync(x =>
            x.RequesterOrderReference == requesterOrderReference);
    }
}