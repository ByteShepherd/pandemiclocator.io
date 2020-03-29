using System.ComponentModel.DataAnnotations;
using pandemiclocator.io.database.abstractions.Enums;

namespace api.pandemiclocator.io.Infra.Commands
{
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
}