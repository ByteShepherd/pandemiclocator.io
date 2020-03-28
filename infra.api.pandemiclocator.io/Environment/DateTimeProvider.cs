using System;
using pandemiclocator.io.abstractions.Environment;

namespace infra.api.pandemiclocator.io.Environment
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
