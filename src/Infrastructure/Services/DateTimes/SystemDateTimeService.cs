using FluentPOS.Application.Abstractions.DateTimes;
using System;

namespace FluentPOS.Infrastructure.Services.DateTimes
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}