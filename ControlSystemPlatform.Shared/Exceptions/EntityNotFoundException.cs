namespace ControlSystemPlatform.Shared.Exceptions
{
    public class EntityNotFoundException(string nameOfIdentity, string id) : Exception;
}