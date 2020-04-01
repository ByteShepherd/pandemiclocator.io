using System.Threading;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.environment.abstractions;


namespace api.pandemiclocator.io.Infra.Controllers
{
    public abstract class PandemicWithContextControllerBase : PandemicControllerBase
    {
        protected PandemicWithContextControllerBase(IHostInstanceProvider hostInstance, IDateTimeProvider dateTimeProvider) 
            : base(hostInstance, dateTimeProvider)
        {
        }
    }
}