using ControlSystemPlatform.BLL.Abstractions;

namespace ControlSystemPlatform.BLL.TransportOrderDomain.Handlers;

public static class TransportOrderErrors
{
    public static Error InvalidSkuOnOrderItem = new Error("TransportOrder.InvalidSku",
        "an order item contains a SKU that is not recognized by the system");

    public static Error DuplicatedOrder = new Error("TransportOrder.DuplicatedOrder",
        "The order is already created");

    public static Error InvalidDestinationLocation = new Error("TransportOrder.InvalidDestination",
        "The destination is not valid");
}