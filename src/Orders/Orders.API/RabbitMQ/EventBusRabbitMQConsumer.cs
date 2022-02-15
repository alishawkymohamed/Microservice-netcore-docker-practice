using AutoMapper;
using EventBusrabbitMQ;
using EventBusrabbitMQ.Common;
using EventBusrabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Orders.Application.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Orders.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _rabbitMQConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusRabbitMQConsumer(IRabbitMQConnection rabbitMQConnection, IMediator mediator, IMapper mapper)
        {
            this._rabbitMQConnection = rabbitMQConnection;
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public void Consume()
        {
            var channel = _rabbitMQConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

                var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _rabbitMQConnection.Dispose();
        }
    }
}
