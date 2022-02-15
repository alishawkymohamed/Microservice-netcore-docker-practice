using RabbitMQ.Client;
using System;

namespace EventBusrabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        public bool IsConnected { get; }
        public bool TryConnect();
        IModel CreateModel();
    }
}
