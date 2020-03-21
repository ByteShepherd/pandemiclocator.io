using System;
using System.ComponentModel.DataAnnotations;
using api.pandemiclocator.io.Infra.Data.Enums;

namespace api.pandemiclocator.io.Infra.Data.Documents
{
    public class HealthReport
    {
        [Required]
        [MinLength(1)]
        public string Identifier { get; }

        [Required]
        public HealthStatus Status { get; }

        [Required]
        public ReportSource Source { get; }

        [Required]
        public (double latidude, double longitude) Coordinate { get; }

        [Required]
        public DateTime When { get; }

        public HealthReport(string identifier, HealthStatus status, ReportSource source, (double latidude, double longitude) coordinate, DateTime when)
        {
            Identifier = identifier;
            Status = status;
            Source = source;
            Coordinate = coordinate;
            When = when;
        }
    }
}
