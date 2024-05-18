namespace ControlSystemPlatform.Shared.Exceptions;

public class CustomValidationException(string msg, string id = null) : Exception;