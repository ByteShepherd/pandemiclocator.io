using System;

namespace pandemiclocator.io.model.abstractions
{
    public class HealthReport
    {
        public HealthReport()
        {
            Id = Guid.NewGuid();
        }

        public HealthReport(string identifier, HealthStatus status, int quantity, ReportSource source, ReportLocation location, DateTime when)
        {
            Id = Guid.NewGuid();
            Identifier = identifier;
            Quantity = quantity;
            Status = status;
            Source = source;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            When = when;
        }

        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public int Quantity { get;  set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime When { get; set; }
    }
}
