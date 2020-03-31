using System;

namespace pandemiclocator.io.environment.abstractions
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}