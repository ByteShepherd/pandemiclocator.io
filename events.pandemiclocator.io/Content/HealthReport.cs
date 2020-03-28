using System;
using Amazon.DynamoDBv2.DataModel;
using infra.api.pandemiclocator.io.Data.Enums;
using infra.api.pandemiclocator.io.Data.Tables;

namespace events.pandemiclocator.io.Content
{
    [DynamoDBTable(nameof(HealthReport))]
    public class HealthReport : IPandemicDynamoTable
    {
        public HealthReport()
        {
            (this as IPandemicDynamoTable).GenerateNewId();
        }

        public HealthReport(string identifier, HealthStatus status, int quantity, ReportSource source, double latitude, double longitude, DateTime when)
        {
            (this as IPandemicDynamoTable).GenerateNewId();
            Identifier = identifier;
            Quantity = quantity;
            Status = status;
            Source = source;
            Latitude = latitude;
            Longitude = longitude;
            When = when;
        }

        [DynamoDBHashKey]
        public string Id { get; set; }

        public string Identifier { get; set; }
        public int Quantity { get; protected set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime When { get; set; }

        
    }
}
