using System;
using System.Threading;
using System.Threading.Tasks;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.model.abstractions;
using pandemiclocator.io.services.abstractions;

namespace pandemiclocator.io.services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IDbPandemicConnection _pandemicDbConnection;
        public GeolocationService(IDbPandemicConnection pandemicDbConnection)
        {
            _pandemicDbConnection = pandemicDbConnection;
        }

        //Trying https://ourcodeworld.com/articles/read/1019/how-to-find-nearest-locations-from-a-collection-of-coordinates-latitude-and-longitude-with-php-mysql
        public async Task<PandemicReport[]> GetReportsNearByAsync(ReportLocation currentLocation, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _pandemicDbConnection?.Dispose();
        }
    }
}
