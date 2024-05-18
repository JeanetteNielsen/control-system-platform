using ControlSystemPlatform.DAL.Enities;
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
        if (!context.Items.Any())
        {
            context.Items.Add(new Item
            {
                SKU = "SKU0123",
                Description = "Test item1",
                Dimensions = new Dimensions { Height = 23m, Length = 50m, Width = 42m },
                Weight = 1200m
            });

            context.Items.Add(new Item
            {
                SKU = "SKU0042",
                Description = "Test item2",
                Dimensions = new Dimensions { Height = 11m, Length = 51m, Width = 20m },
                Weight = 1200m
            });

            context.Items.Add(new Item
            {
                SKU = "SKU0101",
                Description = "Test item3",
                Dimensions = new Dimensions { Height = 100m, Length = 5m, Width = 4m },
                Weight = 1200m
            });
            context.SaveChanges();
        }

        //TODO: import items, locations, warehouse layouts ect
    }
}