using Microsoft.AspNetCore.Identity;
using System;

namespace FluentPOS.Infrastructure.Identity
{
    //Map this to ApplicationUser in the Application Layer.
    public class ExtendedIdentityUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}