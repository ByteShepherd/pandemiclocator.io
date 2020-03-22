using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api.pandemiclocator.io.Infra.Controllers
{
    public class PandemicResponse<T>
    {
        public int Status { get; }
        public T Data { get; }
        public string Message { get; }

        public PandemicResponse(HttpStatusCode status, T data, string message)
        {
            Status = (int)status;
            Data = data;
            Message = message;
        }
    }

    public static class PandemicResponseExtensions
    {
        public static PandemicResponse<T> ToSuccessPandemicResponse<T>(this T source, string message = null)
        {
            return new PandemicResponse<T>(HttpStatusCode.OK, source, message);
        }

        public static PandemicResponse<T> ToErrorPandemicResponse<T>(this T source, string message)
        {
            return new PandemicResponse<T>(HttpStatusCode.InternalServerError, source, message);
        }

        public static PandemicResponse<T> ToBadRequestPandemicResponse<T>(this T source, string message)
        {
            return new PandemicResponse<T>(HttpStatusCode.BadRequest, source, message);
        }

        public static PandemicResponse<T> ToNotFoundPandemicResponse<T>(this T source)
        {
            return new PandemicResponse<T>(HttpStatusCode.NotFound, source, null);
        }
    }
}
