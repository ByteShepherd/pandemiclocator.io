using System;
using System.Collections.Generic;
using System.Text;
using infra.api.pandemiclocator.io.Queue;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace events.pandemiclocator.io
{
    public class NewHealthReportConsumer : EventingBasicConsumer, IPandemicEvent
    {
        public NewHealthReportConsumer(IModel channel) : base(channel)
        {
            channel.InitializeChannelForHealthReport();
        }
    }
}
