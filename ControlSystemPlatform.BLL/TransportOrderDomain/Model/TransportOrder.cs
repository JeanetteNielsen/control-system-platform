using ControlSystemPlatform.DAL.Enities;

namespace ControlSystemPlatform.BLL.TransportOrderDomain.Model;

public class TransportOrder
{
    public Guid OrderID { get; set; }
    public Guid SourceLocationId { get; set; }
    public Guid DestinationLocationId { get; set; }
    public List<ItemDetail> Items { get; set; } = new List<ItemDetail>();
    public PriorityLevel Priority { get; set; }
    public Guid RequestedBy { get; set; }
    public DateTime RequiredCompletionTime { get; set; }
    public Guid AssignedResource { get; set; }
    public OrderStatus Status { get; set; }
    public InProgressStatus HandlingStatus { get; set; }
    public RouteInfo RoutingInformation { get; set; }
    public string HandlingInstructions { get; set; }
    public List<Events> Timestamps { get; set; } = new List<Events>();
    public string Remarks { get; set; }
    public List<TransportOrder> LinkedOrders { get; set; } = new List<TransportOrder>();
    public List<ErrorHandlingProtocol> ErrorHandlingProtocols { get; set; } = new List<ErrorHandlingProtocol>();
}

public class ItemDetail
{
    public Guid Id { get; set; }
    public string SKU { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Weight { get; set; }
    public Dimensions ItemDimensions { get; set; }
    public string SpecialHandlingInstructions { get; set; }
}

public record Dimensions
{
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public class RouteInfo
{
    public List<string> Waypoints { get; set; } = new List<string>();
    public string AlternatePath { get; set; }
    public string SpecialNavigationInstructions { get; set; }
}

public class Events
{
    public DateTime EventTime { get; set; }
    public string EventDescription { get; set; }
}

public class ErrorHandlingProtocol
{
    public string Issue { get; set; }
    public string ResolutionSteps { get; set; }
}