using FluentPOS.Application.Abstractions.DI;
using System;

namespace FluentPOS.Application.Abstractions.DateTimes
{
    public interface IDateTimeService : IApplicationService
    {
        DateTime UtcNow { get; }
    }
}