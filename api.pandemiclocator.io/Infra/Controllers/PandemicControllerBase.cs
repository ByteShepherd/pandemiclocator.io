using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using infra.api.pandemiclocator.io;

using Microsoft.AspNetCore.Mvc;
using pandemiclocator.io.abstractions.Environment;

namespace api.pandemiclocator.io.Infra.Controllers
{
    [Route("[controller]/[action]")]
    public abstract class PandemicControllerBase : ControllerBase
    {
        public IHostInstanceProvider HostInstance { get; }
        public IDateTimeProvider DateTimeProvider { get; }

        protected PandemicControllerBase(IHostInstanceProvider hostInstanceProvider, IDateTimeProvider dateTimeProvider)
        {
            HostInstance = hostInstanceProvider;
            DateTimeProvider = dateTimeProvider;
        }

        [HttpGet]
        public string HealthCheck()
        {
            var cloud = HostInstance.IsRunningOnCloud ? "@AWS" : "@Docker";
            return $"[{HostInstance.HostInstanceId}{cloud}] {DateTimeProvider.Now}";
        }
    }


    
}