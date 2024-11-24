using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.API.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager):BaseApiController
    {
        [HttpPost("login")] // POST: api/Account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var response=await serviceManager.AuthService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("register")] // POST: api/Account/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var response=await serviceManager.AuthService.RegisterAsync(model);
            return Ok(response);
        }

        [HttpGet] // GET: api/Account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }

    }
}
