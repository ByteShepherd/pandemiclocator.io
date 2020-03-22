using System;
using System.ComponentModel.DataAnnotations;
using api.pandemiclocator.io.Infra.Data.Documents;
using api.pandemiclocator.io.Infra.Data.Enums;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class ReadHealthReportCommand
    {
        public ReadHealthReportCommand(HealthStatus status, int quantity, ReportSource source, double latitude, double longitude, DateTime when)
        {
            Quantity = quantity;
            Status = status;
            Source = source;
            Latitude = latitude;
            Longitude = longitude;
            When = when;
        }

        public int Quantity { get; }
        public HealthStatus Status { get; }
        public ReportSource Source { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public DateTime When { get; }
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
