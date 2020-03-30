using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.database.abstractions.Models;

namespace pandemiclocator.io.database
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IDynamoDbProvider _dynamoDbProvider;
        public GeolocationService(IDynamoDbProvider dynamoDbProvider)
        {
            _dynamoDbProvider = dynamoDbProvider;
        }

        //Trying https://ourcodeworld.com/articles/read/1019/how-to-find-nearest-locations-from-a-collection-of-coordinates-latitude-and-longitude-with-php-mysql
        public async Task<PandemicReport[]> GetReportsNearByAsync(ReportLocation currentLocation, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
