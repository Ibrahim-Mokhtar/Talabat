using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Domain.Entites.Identity;

namespace Talabat.Core.Application.Services.Auth
{
    internal class AuthService (IOptions<JWTSettings> jwtSettings,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager): IAuthService
    {
        private readonly JWTSettings _jwtSettings=jwtSettings.Value;
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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var privateClaims = new[]
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.DispalyName)
            }.Union(await userManager.GetClaimsAsync(user)).ToList();

            foreach (var role in await userManager.GetRolesAsync(user))
                privateClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"));

            var toketObj = new JwtSecurityToken(

                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: privateClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(toketObj);
        }
    }
}
