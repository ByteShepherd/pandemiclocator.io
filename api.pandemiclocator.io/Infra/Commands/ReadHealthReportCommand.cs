using System;
using pandemiclocator.io.database.abstractions.Models;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class ReadHealthReportCommand
    {
        public ReadHealthReportCommand()
        {
        }

        public ReadHealthReportCommand(HealthStatus status, int quantity, ReportSource source, ReportLocation location, DateTime when)
        {
            Quantity = quantity;
            Status = status;
            Source = source;
            Location = location;
            When = when;
        }

        public int Quantity { get; set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public ReportLocation Location { get; set; }
        public DateTime When { get; set; }
    }
}