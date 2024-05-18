using System.Net;
using FluentAssertions;
using ControlSystemPlatform.DAL;
using Tools.Test.API;
using Tools.Test.AutoData;
using Xunit;
using ControlSystemPlatform.DAL.Enities;
using ControlSystemPlatform.BLL.TransportOrderDomain.Handlers;

namespace ControlSystemPlatform.Server.Test
{
    public class TransportOrderApiTest() : ApiTestBase<WarehouseDbContext>("transportorder")
    {
        protected WarehouseTestDb TestDb;

        protected override void SetupTestDb(WarehouseDbContext dbContext)
        {
            TestDb = new WarehouseTestDb(dbContext);
        }


        [Theory, AutoFakeData]
        public async Task GivenATransportOrder_WhenCallingPost_ThenTransportOrderIsPersistet(List<Item> items,
            CreateTransportOrderCommand.CreateRequest request)
        {
            // Arrange
            TestDb.WithItems(items);
            var correctedItems = items.Select((t, i) => request.Items[i] with { SKU = t.SKU }).ToList();
            request = request with { Items = correctedItems };

            // Act
            var result = await PostAsync("", request);

            // Assert
            result.EnsureSuccessStatusCode();

            var resultFromDb =
                TestDb.Context.TransportOrder.SingleOrDefault(x => x.RequesterOrderReference == request.OrderId);
            resultFromDb.Should().NotBeNull();

            //TODO: assert properties and states
        }


        [Theory, AutoFakeData]
        public async Task GivenATransportOrderWithInvalidItemSku_WhenCallingPost_ThenBadRequestIs(
            CreateTransportOrderCommand.CreateRequest request)
        {
            // Arrange

            // Act
            var result = await PostAsync("", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoFakeData]
        public async Task GivenTheSameTransportOrderTwice_WhenCallingPost_ThenBadRequestIsReturned(List<Item> items,
            CreateTransportOrderCommand.CreateRequest request)
        {
            // Arrange
            TestDb.WithItems(items);
            var correctedItems = items.Select((t, i) => request.Items[i] with { SKU = t.SKU }).ToList();
            request = request with { Items = correctedItems };

            // Act
            await PostAsync("", request);
            var result = await PostAsync("", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}