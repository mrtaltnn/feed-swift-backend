using System.Net;

namespace Shared.Helper.Exceptions;

public sealed class AppException : Exception
{
    public string Title { get; set; }
    public new string Message { get; set; }
    public new object? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

    public AppException(string title, string message) : base(message)
    {
        Title = title;
        Message = message;
    }

    public AppException(string title, string message, HttpStatusCode statusCode) : base(message)
    {
        Title = title;
        StatusCode = statusCode;
        Message = message;
    }  
    public AppException(string title, string message,object data, HttpStatusCode statusCode) : base(message)
    {
        Title = title;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    } 
    public AppException(string message,object data, HttpStatusCode statusCode) : base(message)
    {
        Title = "Model Validation Error";
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public AppException(string title, string message, HttpStatusCode statusCode, Exception inner) : base(message,
        inner)
    {
        Title = title;
        StatusCode = statusCode;
        Message = message;
    }
}