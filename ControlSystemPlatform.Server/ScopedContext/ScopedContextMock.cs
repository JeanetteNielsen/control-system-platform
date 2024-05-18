using ControlSystemPlatform.Shared;

namespace ControlSystemPlatform.Server.ScopedContext
{
    /// <summary>
    /// We are not implementing user management is this template, this is purely mock data
    /// </summary>
    public class ScopedContextMock : IScopedContext
    {
        public string UserName => "TestUser";
        public Guid UserId => Guid.NewGuid();
    }
}