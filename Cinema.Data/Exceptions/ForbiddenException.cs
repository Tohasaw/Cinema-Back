using System.Net;

namespace Cinema.Data.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public ForbiddenException(string message) : base(message)
    {
    }
}