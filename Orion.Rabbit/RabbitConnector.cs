using EasyNetQ;
using Microsoft.Extensions.Options;
using System;

namespace Orion.Rabbit
{
    public class RabbitConnector : IDisposable
    {
        private readonly IOptions<RabbitConfig> _appConfig;

        /// <summary>
        /// IBus contains connection to RabbitMQ.
        /// </summary>
        public IBus IBus { get; }

        /// <summary>
        /// Creates connection to Rabbit. There should be only one connection for an aplication.
        /// </summary>
        /// <param name="appConfig">Important part of this configuration is ConnectionString</param>
        public RabbitConnector(IOptions<RabbitConfig> appConfig)
        {
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));

            try
            {
                IBus = RabbitHutch.CreateBus(_appConfig.Value.ConnectionString);
            }
            catch
            {
                // TODO: log error message
                throw;
            }
        }

        public void Dispose()
        {
            IBus.SafeDispose();
        }
    }
}
