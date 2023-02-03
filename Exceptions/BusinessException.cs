using System;

namespace LibraryWebApi.Exceptions;

internal class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }

    public override string ToString()
    {
        return $"{Message}\n\n{base.ToString()}";
    }
}