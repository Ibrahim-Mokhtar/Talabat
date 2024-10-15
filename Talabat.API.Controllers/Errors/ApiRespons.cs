using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.API.Controllers.Errors
{
    internal class ApiRespons
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiRespons(int statusCode, string? message =null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Errors are the bath to the dark side. Errors lead to anger. anger lead to hate. hate lead to carrer change",
                _ => null
            };

        }
    }
}
