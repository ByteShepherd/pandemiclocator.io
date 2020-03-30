using System;
using Amazon.DynamoDBv2.DataModel;

namespace pandemiclocator.io.database.abstractions.Models
{
    [DynamoDBTable(nameof(HealthReport))]
    public class HealthReport : IPandemicDynamoTable
    {
        public HealthReport()
        {
            (this as IPandemicDynamoTable).GenerateNewId();
        }

        public HealthReport(string identifier, HealthStatus status, int quantity, ReportSource source, ReportLocation location, DateTime when)
        {
            (this as IPandemicDynamoTable).GenerateNewId();
            Identifier = identifier;
            Quantity = quantity;
            Status = status;
            Source = source;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            When = when;
        }

        [DynamoDBHashKey]
        public string Id { get; set; }

        public string Identifier { get; set; }
        public int Quantity { get;  set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime When { get; set; }
    }
}
