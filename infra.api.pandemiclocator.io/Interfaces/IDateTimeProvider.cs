using System;

namespace infra.api.pandemiclocator.io.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}