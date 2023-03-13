using System.Net;
using System.Runtime.Serialization;

namespace Cinema.Data.Exceptions;

[Serializable]
internal class BadRequestException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    
    public BadRequestException(string message) : base(message)
    {
    }
}