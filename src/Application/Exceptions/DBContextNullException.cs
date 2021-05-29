using System;

namespace FluentPOS.Application.Exceptions
{
    public class DBContextNullException : Exception
    {
        public DBContextNullException() : base("Fetching DBContext Failed.")
        {
        }
    }
}