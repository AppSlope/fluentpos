using System.ComponentModel.DataAnnotations;

namespace FluentPOS.Application.Requests.Users
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}