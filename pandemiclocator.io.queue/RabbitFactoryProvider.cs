using System;
using pandemiclocator.io.queue.abstractions;
using RabbitMQ.Client;

namespace pandemiclocator.io.queue
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

        public IBasicProperties ChannelBasicProperties { get; private set; }

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

                            //Source: https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
                            //Since We have made our QueuDeclare as Durable (in QueueHealthReportChannelExtensions / InitializeChannelForHealthReport)
                            //At this point we're sure that the task_queue queue won't be lost even if RabbitMQ restarts.Now we need to mark our messages as
                            //persistent - by setting IBasicProperties.SetPersistent to true.
                            ChannelBasicProperties = _channel.CreateBasicProperties();
                            ChannelBasicProperties.Persistent = true;

                            InitializeChannel();
                        }
                    }
                }

                return _channel;
            }
        }

        protected virtual void InitializeChannel()
        {
            throw new NotImplementedException();
    }

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