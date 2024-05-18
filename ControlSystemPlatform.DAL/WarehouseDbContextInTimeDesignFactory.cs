using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ControlSystemPlatform.DAL;

/// <summary>
/// This is used for migrations design time and not when running the code
/// </summary>
public class WarehouseDbContextInTimeDesignFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
{
    public WarehouseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=(LocalDb)\\MSSQLLocalDB; Initial Catalog=WarehouseDb; Integrated Security=True");

        return new WarehouseDbContext(optionsBuilder.Options, null);
    }
}