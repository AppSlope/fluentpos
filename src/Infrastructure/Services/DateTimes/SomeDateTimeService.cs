using FluentPOS.Application.Abstractions.DateTimes;
using System;

namespace FluentPOS.Infrastructure.Services.DateTimes
{
    public class SomeDateTimeService : IDateTimeService
    {
        public DateTime UtcNow => throw new NotImplementedException();
    }
}