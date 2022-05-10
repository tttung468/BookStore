using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace BookStore.Common.Exceptions;

[Serializable]
public class BookStoreException : Exception
{
    //required constructor
    public BookStoreException() : this(null, null)
    {
    }

    //required constructor
    public BookStoreException(string message) : this(message, null)
    {
    }

    //required constructor
    public BookStoreException(string? message, Exception? innerException) : this(ExceptionErrorCode.TechnicalError,
        message, innerException)
    {
    }

    public BookStoreException(int errorCode) : this(errorCode, null)
    {
    }

    public BookStoreException(int errorCode, string? message) : this(errorCode, message, null)
    {
    }

    public BookStoreException(int errorCode, string? message, Exception? innerException) : base(message,
        innerException)
    {
        ErrorCode = errorCode;
        Guid = Guid.NewGuid();
    }

    protected BookStoreException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ErrorCode = info.GetInt32(nameof(ErrorCode));
        Guid = (Guid)info.GetValue(nameof(Guid), typeof(Guid));
        Parameters = (List<string>)info.GetValue(nameof(Parameters), typeof(List<string>));
    }

    public int ErrorCode { get; set; }

    public Guid Guid { get; protected set; }

    public List<string> Parameters { get; } = new List<string>();

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(ErrorCode), ErrorCode);
        info.AddValue(nameof(Guid), Guid);
        info.AddValue(nameof(Parameters), Parameters);

        if (InnerException != null && InnerException.Data.Count > 0 &&
            InnerException.Data is Dictionary<object, object> data)
        {
            foreach (var key in data.Keys.ToList())
            {
                if (data[key] is JArray)
                {
                    InnerException.Data[key] = (InnerException.Data[key] as JArray)?.ToString();
                }
            }
        }

        base.GetObjectData(info, context);
    }
}
