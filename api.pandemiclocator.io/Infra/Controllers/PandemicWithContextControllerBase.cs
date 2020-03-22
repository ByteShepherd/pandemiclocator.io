using System.Threading;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Interfaces;

namespace api.pandemiclocator.io.Infra.Controllers
{
    public abstract class PandemicWithContextControllerBase : PandemicControllerBase
    {
        protected IPandemicContext Context { get; }
        protected PandemicWithContextControllerBase(IPandemicContext context, IHostInstanceProvider hostInstance, IDateTimeProvider dateTimeProvider) 
            : base(hostInstance, dateTimeProvider)
        {
            Context = context;
        }
    }
}