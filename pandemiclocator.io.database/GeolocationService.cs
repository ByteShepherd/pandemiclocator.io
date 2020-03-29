using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IDynamoDbProvider _dynamoDbProvider;
        public GeolocationService(IDynamoDbProvider dynamoDbProvider)
        {
            _dynamoDbProvider = dynamoDbProvider;
        }

        public async Task<PandemicLocation[]> GetReportsNearByAsync(PandemicLocation currentLocation)
        {
            throw new NotImplementedException();
        }
    }
}
