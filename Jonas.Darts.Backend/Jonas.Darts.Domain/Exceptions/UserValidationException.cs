using System;

namespace Jonas.Darts.Domain.Exceptions;

public class UserValidationException : Exception
{
    public UserValidationException(string message) : base(message) { }
}
