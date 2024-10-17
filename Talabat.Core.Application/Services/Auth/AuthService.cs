using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Domain.Entites.Identity;

namespace Talabat.Core.Application.Services.Auth
{
    internal class AuthService (UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager): IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);
            if (user != null) throw new BadRequestException("Invalid Login");
            var result=await signInManager.CheckPasswordSignInAsync(user,model.Password,lockoutOnFailure:true);
            if(!result.Succeeded) throw new BadRequestException("Invalid Login");
            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DispalyName,
                Email = user.Email!,
                Token = "This Will be JWT Token"
            };
            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {

                DispalyName = model.DisplayName,
                Email = model.Email!,
                UserName=model.UserName,
                PhoneNumber=model.Phone
            };
            var result =await userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DispalyName,
                Email = user.Email!,
                Token = "This Will be JWT Token"
            };
            return response;
        }
    }
}
