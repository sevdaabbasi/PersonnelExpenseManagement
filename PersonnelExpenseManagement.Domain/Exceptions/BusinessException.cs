using System;
using System.Runtime.Serialization;

namespace PersonnelExpenseManagement.Domain.Exceptions;

[Serializable]
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}