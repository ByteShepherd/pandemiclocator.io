using System.Threading;
using infra.api.pandemiclocator.io;

namespace api.pandemiclocator.io.Infra.Controllers
{
    public abstract class PandemicWithContextControllerBase : PandemicControllerBase
    {
        protected IPandemicContext Context { get; }
        protected PandemicWithContextControllerBase(IPandemicContext context)
        {
            Context = context;
        }
    }
}