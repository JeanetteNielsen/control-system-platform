namespace ControlSystemPlatform.Shared;

public interface IUpdateableEntity
{
    public Guid UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}