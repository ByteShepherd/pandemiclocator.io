using System;
using System.ComponentModel.DataAnnotations;
using pandemiclocator.io.database.abstractions;
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

    public class CreateHealthReportCommand
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        [MinLength(1)]
        public string Identifier { get; set; }

        [Required]
        public HealthStatus Status { get; set; }

        [Required]
        public ReportSource Source { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }

    public static class CreateHealthReportCommandExtensions
    {
        public static ReadHealthReportCommand ToReadCommand(this HealthReport model)
        {
            if (model == null)
            {
                return null;
            }

            return new ReadHealthReportCommand
            (
                model.Status,
                model.Quantity,
                model.Source,
                model.Latitude,
                model.Longitude,
                model.When
            );
        }

        public static HealthReport ToModel(this CreateHealthReportCommand command)
        {
            if (command == null)
            {
                return null;
            }

            if (command.Quantity < 1 || command.Quantity > 10)
            {
                command.Quantity = 1;
            }

            return new HealthReport
            (
                command.Identifier,
                command.Status,
                command.Quantity,
                command.Source,
                command.Latitude,
                command.Longitude,
                DateTime.UtcNow
            );
        }
    }
}
