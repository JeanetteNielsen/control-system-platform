using ControlSystemPlatform.DAL;
using ControlSystemPlatform.DAL.Enities;

namespace ControlSystemPlatform.Server.Test;

public class WarehouseTestDb
{
    public WarehouseDbContext Context { get; }

    public WarehouseTestDb(WarehouseDbContext context)
    {
        Context = context;
    }

    public WarehouseTestDb WithItems(List<Item> items)
    {
        Context.Items.AddRange(items);
        Context.SaveChanges();
        return this;
    }
}