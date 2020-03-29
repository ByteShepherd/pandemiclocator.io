using System;
using pandemiclocator.io.database.abstractions.Enums;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class ReadHealthReportCommand
    {
        public ReadHealthReportCommand()
        {
        }

        public ReadHealthReportCommand(HealthStatus status, int quantity, ReportSource source, double latitude, double longitude, DateTime when)
        {
            Quantity = quantity;
            Status = status;
            Source = source;
            Latitude = latitude;
            Longitude = longitude;
            When = when;
        }

        public int Quantity { get; set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime When { get; set; }
    }
}