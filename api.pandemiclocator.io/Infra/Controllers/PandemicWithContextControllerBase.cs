using System.Threading;
using infra.api.pandemiclocator.io;
using infra.api.pandemiclocator.io.Interfaces;

namespace api.pandemiclocator.io.Infra.Controllers
{
    public abstract class PandemicWithContextControllerBase : PandemicControllerBase
    {
        protected IDynamoDbProvider Context { get; }
        protected PandemicWithContextControllerBase(IDynamoDbProvider context, IHostInstanceProvider hostInstance, IDateTimeProvider dateTimeProvider) 
            : base(hostInstance, dateTimeProvider)
        {
            Context = context;
        }
    }
}