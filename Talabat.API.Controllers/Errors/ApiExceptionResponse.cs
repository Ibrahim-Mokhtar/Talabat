using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Talabat.API.Controllers.Errors
{
    public class ApiExceptionResponse:ApiRespons
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int statusCode,string?message=null,string? details=null)
        :base(statusCode,message)
        {
            Details = details;
        }

       
    }
}
