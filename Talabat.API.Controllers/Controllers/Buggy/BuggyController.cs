using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.API.Controllers.Errors;
using Talabat.APIs.Controllers.Base;

namespace Talabat.API.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")] // GET : api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiRespons(404)); // 404
        }

        [HttpGet("servererror")] // GET : api/buggy/servererror
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }

        [HttpGet("badrequest")] // GET : api/buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiRespons(400)); // 400
        }

        [HttpGet("badrequest/{id}")] // GET : api/buggy/badrequest/five
        public IActionResult GetValidationError(int id) // => 401
        {
            return Ok();
        }

        [HttpGet("unauthorized")] // GET : api/buggy/unauthorized
        public IActionResult GetUnauthorizerError()
        {
            return Unauthorized(new ApiRespons(401)); // 401
        }

    }
}
