using System;

namespace pandemiclocator.io.abstractions.Environment
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}