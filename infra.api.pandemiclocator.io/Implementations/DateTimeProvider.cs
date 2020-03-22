using System;
using System.Collections.Generic;
using System.Text;
using infra.api.pandemiclocator.io.Interfaces;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
