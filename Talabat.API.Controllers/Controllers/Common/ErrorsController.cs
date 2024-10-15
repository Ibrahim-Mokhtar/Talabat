using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Talabat.API.Controllers.Errors;
using Talabat.APIs.Controllers.Base;

namespace Talabat.API.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : BaseApiController
    {
        [HttpGet]
        public IActionResult Error(int Code) 
        {
            if (Code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiRespons((int)HttpStatusCode.NotFound, $"The requested endpoint:{Request.Path} is not found");
                return NotFound(response);
            }
            return StatusCode(Code);
        }
    }
}
