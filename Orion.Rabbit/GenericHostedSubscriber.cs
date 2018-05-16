using EasyNetQ;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Rabbit
{
    public class GenericHostedSubscriber<T> : HostedService where T : class
    {
        private readonly IBus Bus;
        private readonly IOptions<RabbitConfig> _appConfig;

        /// <summary>
        /// Initialize this class
        /// </summary>
        /// <param name="rabbitConnector">This is set in startup.cs by:
        /// services.AddSingleton<RabbitConnector>();</param>
        /// <param name="appConfig">This is set in startup.cs by:
        /// services.Configure<RabbitConfig>(Configuration.GetSection("RabbitConfig"));</param>
        public GenericHostedSubscriber(RabbitConnector rabbitConnector, IOptions<RabbitConfig> appConfig, System.IServiceProvider services)
            :base(services)
        {
            Bus = rabbitConnector?.IBus ?? throw new ArgumentNullException(nameof(rabbitConnector));
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var subscriptionResult = Bus.SubscribeAsync<T>(_appConfig.Value.SubscriptionId, RunScopedService);

            Console.WriteLine("Listening for messages until service is stopped.");
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100, cancellationToken);
            }

            subscriptionResult.SafeDispose();
        }

        private async Task RunScopedService(T message)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService<T>>();

                await scopedProcessingService.HandleMessageAsync(message);
            }
        }

    }
}
