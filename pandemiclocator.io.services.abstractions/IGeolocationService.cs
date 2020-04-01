using System;
using System.Threading;
using System.Threading.Tasks;
using pandemiclocator.io.model.abstractions;

namespace pandemiclocator.io.services.abstractions
{
    public interface IGeolocationService : IDisposable
    {
        Task<PandemicReport[]> GetReportsNearByAsync(ReportLocation currentLocation, CancellationToken cancellationToken);
    }
}
