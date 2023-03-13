using System.Net;
using System.Runtime.Serialization;

namespace Cinema.Data.Exceptions;

[Serializable]
internal class NotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public NotFoundException(string message) : base(message)
    {
    }
}