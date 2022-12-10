using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class BaseResponse<T>
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public Response? ErrorMessage { get; set; }
        public T? Data { get; set; }
    }
}
