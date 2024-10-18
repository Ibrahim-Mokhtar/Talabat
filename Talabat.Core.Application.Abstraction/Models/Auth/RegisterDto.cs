using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        [RegularExpression(
        @"^(?=.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()\-_=+{}[\]:;""'<>?,./\\|~`]).*$",
        ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non-alphanumeric, and be between 6-10 characters.")]
        public required string Password { get; set; }
    }
}
