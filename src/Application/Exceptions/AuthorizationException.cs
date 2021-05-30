using System;

namespace FluentPOS.Application.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base("Authorization Failed.")
        {
        }
    }
}