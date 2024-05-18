using System.Text.Json.Serialization;
using ControlSystemPlatform.BLL.TransportOrderDomain.Handlers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ControlSystemPlatform.DAL;
using ControlSystemPlatform.DAL.Query;
using ControlSystemPlatform.Server.ScopedContext;
using ControlSystemPlatform.Shared;
using ControlSystemPlatform.BLL.TransportOrderDomain;
using ControlSystemPlatform.DAL.Command;

namespace ControlSystemPlatform.Server.Infrastructure
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSerilog();
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IScopedContext, ScopedContextMock>();

            services.AddTransient<ITransportOrderPublisher, TransportOrderPublisher>();
            services.AddTransient<IAddNewTransportOrderCommand, AddNewTransportOrderCommand>();
            services.AddTransient<IGetOrderItemIdsQuery, GetOrderItemIdsQuery>();
            services.AddTransient<IGetTransportOrderOrDefault, GetTransportOrderOrDefault>();

            // Since the app is so small, there is no ref to BLL yet.
            // The following lines assures the assembly is loaded before the mediator is registered to allow for the handler to me registered.
            var t = typeof(CreateTransportOrderCommandHandler);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            SetupWarehouseDb(services);
        }

        protected virtual void SetupWarehouseDb(IServiceCollection services)
        {
            services.AddDbContext<WarehouseDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:WarehouseDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();

            // TODO: setup Authentication

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public virtual void SeedData(IServiceProvider services)
        {
            var context = services.GetService<WarehouseDbContext>();
            WarehouseDbSeed.Migrate(context);
            WarehouseDbSeed.Seed(context);
        }
    }

    // TODO: Teststartup.cs will nok work if moved to test project. Investigate how this can be corrected, since it does not belong here.
    public class TestStartup(IConfiguration configuration) : Startup(configuration)
    {
        protected override void SetupWarehouseDb(IServiceCollection services)
        {
            services.AddDbContext<WarehouseDbContext>(options =>
                options.UseInMemoryDatabase("TestingDB"));
        }
    }
}