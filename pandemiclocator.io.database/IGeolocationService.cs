using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public interface IGeolocationService
    {
        Task<PandemicLocation[]> GetReportsNearByAsync(PandemicLocation currentLocation);
    }
}
