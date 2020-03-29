using System.ComponentModel.DataAnnotations;
using pandemiclocator.io.database.abstractions;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class ListHealthReportNearByCommand
    {
        [Required]
        public PandemicLocation CurrentLocation { get; set; }
    }
}