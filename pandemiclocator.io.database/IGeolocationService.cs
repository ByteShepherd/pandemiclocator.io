using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.abstractions.Models;

namespace pandemiclocator.io.database
{
    public interface IGeolocationService
    {
        Task<PandemicReport[]> GetReportsNearByAsync(ReportLocation currentLocation, CancellationToken cancellationToken);
    }
}
