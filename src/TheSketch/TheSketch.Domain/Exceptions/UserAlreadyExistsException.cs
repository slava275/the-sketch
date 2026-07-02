using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Domain.Exceptions;

public class UserException : Exception
{
    public UserException() : base("User already exists.")
    {
    }

    public UserException(string message) : base(message)
    {
    }

    public UserException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
