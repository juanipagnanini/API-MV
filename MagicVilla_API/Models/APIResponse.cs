using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public List<string> ErrorMenssages { get; set; }
        public object Result { get; set; }
    }
}
