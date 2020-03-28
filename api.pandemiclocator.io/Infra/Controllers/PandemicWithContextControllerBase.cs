using System.Threading;
using infra.api.pandemiclocator.io;
using pandemiclocator.io.abstractions.Database;
using pandemiclocator.io.abstractions.Environment;


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