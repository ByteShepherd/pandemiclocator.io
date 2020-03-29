using System;
using pandemiclocator.io.environment.abstractions;

namespace pandemiclocator.io.environment
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
