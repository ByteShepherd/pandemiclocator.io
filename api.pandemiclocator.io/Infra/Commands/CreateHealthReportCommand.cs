using System;
using api.pandemiclocator.io.Infra.Data.Documents;
using api.pandemiclocator.io.Infra.Data.Enums;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class CreateHealthReportCommand
    {
        public string Identifier { get; }
        public HealthStatus Status { get; }
        public ReportSource Source { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public CreateHealthReportCommand(string identifier, HealthStatus status, ReportSource source, double latitude, double longitude)
        {
            Identifier = identifier;
            Status = status;
            Source = source;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public static class CreateHealthReportCommandExtensions
    {
        public static HealthReport ToModel(this CreateHealthReportCommand command)
        {
            if (command == null)
            {
                return null;
            }

            return new HealthReport
            (
                command.Identifier,
                command.Status,
                command.Source,
                (command.Latitude, command.Longitude),
                DateTime.UtcNow
            );
        }
    }
}
