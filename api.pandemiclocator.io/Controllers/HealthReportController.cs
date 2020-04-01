using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using api.pandemiclocator.io.Infra.Commands;
using api.pandemiclocator.io.Infra.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using pandemiclocator.io.cache.abstractions;
using pandemiclocator.io.database;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.environment.abstractions;
using pandemiclocator.io.model.abstractions;
using pandemiclocator.io.queue.abstractions;
using pandemiclocator.io.services.abstractions;

namespace api.pandemiclocator.io.Controllers
{
    [ApiController]
    public class HealthReportController : PandemicWithContextControllerBase
    {
        private readonly IRedisProvider _cache;
        private readonly IGeolocationService _geolocationService;
        private readonly IHealthReportConsumerPublisher _healthReportConsumerPublisher;

        public HealthReportController(IHealthReportConsumerPublisher healthReportConsumerPublisher, IRedisProvider cache,
            IHostInstanceProvider hostInstanceProvider, IDateTimeProvider dateTimeProvider, IGeolocationService geolocationService) 
            : base(hostInstanceProvider, dateTimeProvider)
        {
            _healthReportConsumerPublisher = healthReportConsumerPublisher;
            _cache = cache;
            _geolocationService = geolocationService;
        }

        //[HttpGet]
        //public async Task<PandemicResponse<ReadHealthReportCommand>> GetWebReport(string key, CancellationToken cancellationToken)
        //{
        //    ReadHealthReportCommand response = null;
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        return response.ToBadRequestPandemicResponse("Invalid Key!");
        //    }

        //    //########## OBTER CACHE
        //    var cachedReport = await _cache.GetCacheAsync<PandemicResponse<ReadHealthReportCommand>>(key, cancellationToken);
        //    if (cachedReport != null)
        //    {
        //        return cachedReport;
        //    }

        //    var model = await Context.GetByIdAsync<HealthReport>(key, cancellationToken);
        //    if (model == null)
        //    {
        //        return response.ToNotFoundPandemicResponse();
        //    }

        //    //########## INSERIR CACHE
        //    response = model.ToReadCommand();
        //    var result = response.ToSuccessPandemicResponse();

        //    if (result != null && result.Status == HttpStatusCode.OK)
        //    {
        //        await _cache.SetCacheAsync(key, result, cancellationToken);
        //    }

        //    return result;
        //}

        [HttpGet]
        public async Task<PandemicResponse<PandemicReport[]>> GetReportsNearBy(ListHealthReportNearByCommand command, CancellationToken cancellationToken)
        {
            PandemicReport[] response = null;
            if (command == null || !TryValidateModel(command, nameof(command)))
            {
                return response.ToBadRequestPandemicResponse("Invalid model");
            }

            try
            {
                response = await _geolocationService.GetReportsNearByAsync(command.Location, cancellationToken);
            }
            catch (Exception err)
            {
                response.ToBadRequestPandemicResponse(err.Message);
            }

            return response.ToSuccessPandemicResponse();
        }

        [HttpPost]
        public async Task<PandemicResponse<CreateHealthReportCommand>> NewWebReport(CreateHealthReportCommand command, CancellationToken cancellationToken)
        {
            if (command == null || !TryValidateModel(command, nameof(command)))
            {
                return command.ToBadRequestPandemicResponse("Invalid model");
            }

            var publishResult = await _healthReportConsumerPublisher.PublishAsync(command.ToModel(), cancellationToken);
            return publishResult.Success ? command.ToSuccessPandemicResponse() : command.ToErrorPandemicResponse(publishResult.Error.Message);
        }
    }
}
