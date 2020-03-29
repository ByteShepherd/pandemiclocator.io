using System.Threading;
using pandemiclocator.io.database.abstractions;
using pandemiclocator.io.environment.abstractions;


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