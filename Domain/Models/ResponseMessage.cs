using System.Net;

namespace Domain.Models
{
    public class ResponseMessage<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
