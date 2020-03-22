using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.pandemiclocator.io.Infra.Commands;
using api.pandemiclocator.io.Infra.Controllers;
using api.pandemiclocator.io.Infra.Data.Documents;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.pandemiclocator.io.Controllers
{
    [ApiController]
    public class HealthReportController : PandemicWithContextControllerBase
    {
        public HealthReportController(IPandemicContext context, IHostInstanceProvider hostInstanceProvider, IDateTimeProvider dateTimeProvider) 
            : base(context, hostInstanceProvider, dateTimeProvider)
        {
        }

        [HttpPost]
        public async Task<(bool isSucces, string message)> WebReportWithCoordinates(CreateHealthReportCommand command, CancellationToken cancellationToken)
        {
            if (command == null || !TryValidateModel(command, nameof(command)))
            {
                return (false, "Report is missing answers");
            }

            var model = command.ToModel();

            await Context.SaveAsync(model, cancellationToken);
            return (true, "Report made");
        }
    }
}
