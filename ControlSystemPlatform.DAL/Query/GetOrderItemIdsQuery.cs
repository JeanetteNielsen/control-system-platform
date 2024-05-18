using Microsoft.EntityFrameworkCore;

namespace ControlSystemPlatform.DAL.Query
{
    public interface IGetOrderItemIdsQuery
    {
        public Task<Dictionary<string, Guid>> Execute(List<string> itemSKUs, CancellationToken cancellationToken);
    }

    public class GetOrderItemIdsQuery(WarehouseDbContext context) : IGetOrderItemIdsQuery
    {
        public async Task<Dictionary<string, Guid>> Execute(List<string> itemSKUs, CancellationToken cancellationToken)
        {
            var items = await context.Items
                .Where(item => itemSKUs.Contains(item.SKU))
                .Select(item => new
                {
                    Id = item.Id,
                    SKU = item.SKU
                }).ToListAsync(cancellationToken);

            return items.ToDictionary(x => x.SKU, x => x.Id);
        }
    }
}