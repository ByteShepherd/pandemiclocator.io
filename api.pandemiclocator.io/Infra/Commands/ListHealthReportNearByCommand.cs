using System.ComponentModel.DataAnnotations;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.model.abstractions;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class ListHealthReportNearByCommand
    {
        [Required]
        public ReportLocation Location { get; set; }
    }
}