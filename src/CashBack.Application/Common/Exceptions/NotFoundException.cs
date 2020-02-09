using System;

namespace CashBack.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }

    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.")
        {
        }
    }
}