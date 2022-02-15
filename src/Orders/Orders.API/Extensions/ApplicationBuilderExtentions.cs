using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.API.RabbitMQ;

namespace Orders.API.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        private static EventBusRabbitMQConsumer Listner { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listner = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopped.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Listner.Consume();
        }

        private static void OnStopping()
        {
            Listner.Disconnect();
        }
    }
}
