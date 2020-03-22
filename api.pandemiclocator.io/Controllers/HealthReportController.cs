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

        [HttpGet]
        public async Task<PandemicResponse<ReadHealthReportCommand>> GetWebReport(string key, CancellationToken cancellationToken)
        {
            ReadHealthReportCommand response = null;
            if (string.IsNullOrEmpty(key))
            {
                return response.ToBadRequestPandemicResponse("Invalid Key!");
            }

            var model = await Context.GetByIdAsync<HealthReport>(key, cancellationToken);
            if (model == null)
            {
                return response.ToNotFoundPandemicResponse();
            }

            response = model.ToReadCommand();

            var result = response.ToSuccessPandemicResponse();
            return result;
        }

        [HttpPost]
        public async Task<PandemicResponse<CreateHealthReportCommand>> NewWebReport(CreateHealthReportCommand command, CancellationToken cancellationToken)
        {
            if (command == null || !TryValidateModel(command, nameof(command)))
            {
                return command.ToBadRequestPandemicResponse("Invalid model");
            }

            var model = command.ToModel();

            await Context.SaveAsync(model, cancellationToken);
            var result = command.ToSuccessPandemicResponse();
            return result;
        }
    }
}
