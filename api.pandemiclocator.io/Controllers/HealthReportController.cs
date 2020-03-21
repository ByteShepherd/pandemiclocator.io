using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.pandemiclocator.io.Infra.Commands;
using api.pandemiclocator.io.Infra.Controllers;
using api.pandemiclocator.io.Infra.Data.Documents;
using infra.api.pandemiclocator.io;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.pandemiclocator.io.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthReportController : PandemicWithContextControllerBase
    {
        public HealthReportController(IPandemicContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<(bool isSucces, string message)> WebReportWithCoordinates(CreateHealthReportCommand command, CancellationToken cancellationToken)
        {
            var model = command.ToModel();
            if (model == null || !TryValidateModel(model, nameof(command)))
            {
                return (false, "Report is missing answers");
            }

            await Context.SaveAsync(model, cancellationToken);
            return (true, "Report made");
        }
    }
}
