using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BookStore.Common.Exceptions;

[Serializable]
public class BookStoreWebApiException : BookStoreException
{
    public BookStoreWebApiException(string message, int statusCode, string response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception? innerException) : base(
            GetMessage(message, statusCode, response, innerException), GetInnerException(response, innerException))
    {
        StatusCode = statusCode;
        Headers = headers;

        if (innerException != null)
        {
            ErrorCode = ExceptionErrorCode.InternalWebApiClientError;
            Response = response;
            return;
        }

        if (TryExtractExceptions<BookStoreException>(response, out var veraJpException))
        {
            ErrorCode = veraJpException.ErrorCode;
            Guid = veraJpException.Guid;
            Parameters.AddRange(veraJpException.Parameters);
        }
        else
        {
            if (StatusCode == 401 || StatusCode == 403)
            {
                ErrorCode = ExceptionErrorCode.AuthenticationError;
            }
            else
            {
                ErrorCode = ExceptionErrorCode.InternalServerError;
            }

            if (!TryExtractExceptions<Exception>(response, out _))
            {
                Response = response;
            }
        }
    }

    protected BookStoreWebApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        StatusCode = info.GetInt32(nameof(StatusCode));
        Response = info.GetString(nameof(Response));
        Headers = (IReadOnlyDictionary<string, IEnumerable<string>>)info.GetValue(nameof(Headers),
            typeof(IReadOnlyDictionary<string, IEnumerable<string>>));
    }

    public int StatusCode { get; }

    public string? Response { get; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

    private static string GetMessage(string message, int statusCode, string response, Exception? innerException)
    {
        if (innerException != null)
        {
            return message;
        }

        if (TryExtractExceptions<BookStoreException>(response, out var veraJpException))
        {
            return veraJpException.Message;
        }

        if (TryExtractExceptions<Exception>(response, out var exception))
        {
            return exception.Message;
        }

        if (statusCode == 401 || statusCode == 403)
        {
            return $"Authentication to the server failed (Status: {statusCode}).";
        }

        return message + "\n\nStatus: " + statusCode + "\nResponse:\n" +
               response.Substring(0, response.Length >= 512 ? 512 : response.Length);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(StatusCode), StatusCode);
        info.AddValue(nameof(Response), Response);
        info.AddValue(nameof(Headers), Headers);

        base.GetObjectData(info, context);
    }

    private static Exception? GetInnerException(string response, Exception? innerException)
    {
        if (innerException != null)
        {
            return innerException;
        }

        if (TryExtractExceptions<BookStoreException>(response, out var veraJpException))
        {
            return veraJpException.InnerException;
        }

        if (TryExtractExceptions<Exception>(response, out var exception))
        {
            return exception;
        }

        return null;
    }

    private static bool TryExtractExceptions<TException>(string response,
        [NotNullWhen(true)] out TException? exception) where TException : Exception
    {
        try
        {
            exception = JsonConvert.DeserializeObject<TException>(response);
            return exception != null;
        }
        catch
        {
            exception = null;
            return false;
        }
    }

    public override string ToString()
    {
        var baseToString = base.ToString();
        return string.IsNullOrWhiteSpace(Response) ? baseToString : $"{baseToString}\n\nHTTP Response:\n\n{Response}";
    }
}
