using Microsoft.EntityFrameworkCore;

namespace ControlSystemPlatform.DAL;

public class WarehouseDbSeed
{
    public static void Migrate(WarehouseDbContext context)
    {
        context.Database.Migrate();
    }

    public static void Seed(WarehouseDbContext context)
    {
        // import items, locations, warehouse layouts ect
    }
}