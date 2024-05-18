using System.ComponentModel.DataAnnotations;

namespace ControlSystemPlatform.DAL.Enities;

public class TransportOrderEntity
{
    public Guid Id { get; set; }
    public Guid RequesterOrderReference { get; set; }
    public Guid SourceLocationId { get; set; }
    public Guid DestinationLocationId { get; set; }
    public List<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();

    // To consider: If we want to sort or order large amount of data based on the enums, the string approach will slow performance
    [MaxLength(20)] public PriorityLevel Priority { get; set; }
    public DateTime RequiredCompletionTime { get; set; }
    public Guid? AssignedResource { get; set; }
    [MaxLength(20)] public OrderStatus Status { get; set; } = OrderStatus.Created;
    [MaxLength(20)] public InProgressStatus HandlingStatus { get; set; } = InProgressStatus.NotStarted;
    public RouteInfo? RoutingInformation { get; set; }

    //Would be a complex object
    [MaxLength(265)] public string? HandlingInstructions { get; set; }
    public List<Events> Timestamps { get; set; } = new List<Events>();
    public string? Remarks { get; set; }

    public List<ErrorHandlingProtocol> ErrorHandlingProtocols { get; set; } = new List<ErrorHandlingProtocol>();

    public Guid RequestedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class OrderItemEntity
{
    public Guid Id { get; set; }
    public Guid TransportOrderEntityId { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    [MaxLength(50)] public string SKU { get; set; }
    [MaxLength(265)] public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Weight { get; set; }
    public Dimensions Dimensions { get; set; }
    [MaxLength(265)] public string SpecialHandlingInstructions { get; set; }
}

public class Dimensions
{
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
}

public enum PriorityLevel
{
    Low,
    Medium,
    High,
    Urgent
}

public enum OrderStatus
{
    Created,
    Assigned,
    InProgress,
    Completed,
    Canceled
}

public enum InProgressStatus
{
    NotStarted,
    OnTime,
    Stuck,
    Rerouting,
    Delayed,
    Completed,
    Canceled
}

public class RouteInfo
{
    public Guid Id { get; set; }
    public Guid TransportOrderEntityId { get; set; }
    public List<Guid> Waypoints { get; set; } = new List<Guid>();
    [MaxLength(265)] public string AlternatePath { get; set; }
    [MaxLength(265)] public string SpecialNavigationInstructions { get; set; }
}

public class Events
{
    public Guid Id { get; set; }
    public Guid TransportOrderEntityId { get; set; }
    public DateTime EventTime { get; set; }
    [MaxLength(265)] public string EventDescription { get; set; }
}

public class ErrorHandlingProtocol
{
    public Guid Id { get; set; }
    [MaxLength(265)] public string Issue { get; set; }
    [MaxLength(265)] public string ResolutionSteps { get; set; }
}