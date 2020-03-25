using infra.api.pandemiclocator.io.Interfaces;
using RabbitMQ.Client;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class RabbitFactoryProvider : IRabbitFactoryProvider
    {
        private readonly IQueueConnectionSection _queueConnectionSection;
        private readonly object _factoryLocker = new object();
        private ConnectionFactory _factory;

        public RabbitFactoryProvider(IQueueConnectionSection queueConnectionSection)
        {
            _queueConnectionSection = queueConnectionSection;
        }

        public ConnectionFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    lock (_factoryLocker)
                    {
                        if (_factory == null)
                        {
                            _factory = new ConnectionFactory()
                            {
                                HostName = _queueConnectionSection.HostName,
                                Port = _queueConnectionSection.Port,
                                UserName = _queueConnectionSection.UserName,
                                Password = _queueConnectionSection.Password
                            };
                        }
                    }
                }

                return _factory;
            }
        }
    }
}