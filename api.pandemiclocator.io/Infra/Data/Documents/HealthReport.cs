using System;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.Internal.Auth;
using api.pandemiclocator.io.Infra.Data.Enums;
using api.pandemiclocator.io.Infra.Data.Tables;

namespace api.pandemiclocator.io.Infra.Data.Documents
{
    [DynamoDBTable(nameof(HealthReport))]
    public class HealthReport : IPandemicDynamoTable
    {
        public HealthReport()
        {
            (this as IPandemicDynamoTable).GenerateNewId();
        }

        public HealthReport(string identifier, HealthStatus status, ReportSource source, double latitude, double longitude, DateTime when)
        {
            (this as IPandemicDynamoTable).GenerateNewId();
            Identifier = identifier;
            Status = status;
            Source = source;
            Latitude = latitude;
            Longitude = longitude;
            When = when;
        }

        [DynamoDBHashKey]
        public string Id { get; set; }

        public string Identifier { get; protected set; }
        public HealthStatus Status { get; protected set; }
        public ReportSource Source { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }
        public DateTime When { get; protected set; }

        
    }
}
