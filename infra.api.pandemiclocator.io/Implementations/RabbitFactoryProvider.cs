using System;
using infra.api.pandemiclocator.io.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace infra.api.pandemiclocator.io.Implementations
{
    public abstract class RabbitFactoryProvider : IQueueFactoryProvider
    {
        private readonly IQueueConnectionSection _queueConnectionSection;
        protected RabbitFactoryProvider(IQueueConnectionSection queueConnectionSection)
        {
            _queueConnectionSection = queueConnectionSection;
        }

        private readonly object _factoryLocker = new object();
        private ConnectionFactory _factory;
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

        private readonly object _connectionLocker = new object();
        private IConnection _connection;
        public IConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (_connectionLocker)
                    {
                        if (_connection == null)
                        {
                            _connection = Factory.CreateConnection();
                            _connection.ConnectionShutdown += ConnectionShutdown;
                        }
                    }
                }

                return _connection;
            }
        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            //TODO:
            throw new NotImplementedException();
        }

        private readonly object _channelLocker = new object();
        private IModel _channel;
        public IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    lock (_channelLocker)
                    {
                        if (_channel == null)
                        {
                            _channel = Connection.CreateModel();
                            InitializeChannel();
                        }
                    }
                }

                return _channel;
            }
        }

        protected abstract void InitializeChannel();

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}