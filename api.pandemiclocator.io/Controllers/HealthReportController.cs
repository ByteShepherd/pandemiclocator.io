using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using api.pandemiclocator.io.Infra.Commands;
using api.pandemiclocator.io.Infra.Controllers;
using events.pandemiclocator.io.Content;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace api.pandemiclocator.io.Controllers
{
    [ApiController]
    public class HealthReportController : PandemicWithContextControllerBase
    {
        private readonly IRedisProvider _cache;

        public HealthReportController(IRedisProvider cache, IDynamoDbProvider context, IHostInstanceProvider hostInstanceProvider, IDateTimeProvider dateTimeProvider) 
            : base(context, hostInstanceProvider, dateTimeProvider)
        {
            _cache = cache;
        }

        [HttpGet]
        public async Task<PandemicResponse<ReadHealthReportCommand>> GetWebReport(string key, CancellationToken cancellationToken)
        {
            ReadHealthReportCommand response = null;
            if (string.IsNullOrEmpty(key))
            {
                return response.ToBadRequestPandemicResponse("Invalid Key!");
            }

            //########## OBTER CACHE
            var cachedReport = await _cache.GetCacheAsync<PandemicResponse<ReadHealthReportCommand>>(key, cancellationToken);
            if (cachedReport != null)
            {
                return cachedReport;
            }

            var model = await Context.GetByIdAsync<HealthReport>(key, cancellationToken);
            if (model == null)
            {
                return response.ToNotFoundPandemicResponse();
            }

            //########## INSERIR CACHE
            response = model.ToReadCommand();
            var result = response.ToSuccessPandemicResponse();

            if (result != null && result.Status == HttpStatusCode.OK)
            {
                await _cache.SetCacheAsync(key, result, cancellationToken);
            }

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
            //TODO: mandar mensageria e quem responde ao evento sera responsavel por adicionar ao DynamoDB...

            var result = command.ToSuccessPandemicResponse();
            return result;
        }
    }
}
