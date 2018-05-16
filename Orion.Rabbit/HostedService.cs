namespace Orion.Rabbit
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class HostedService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private CancellationTokenSource _cts;

        // This part is needed so we can make scopes from this Hosted Service
        public IServiceProvider Services { get; }

        public HostedService(IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services)); ;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Store the task we're executing
            _executingTask = ExecuteAsync(_cts.Token);

            // If the task is completed then return it, 
            // this will bubble cancellation and failure to the caller
            // Otherwise it's running
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _cts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public virtual void Dispose()
        {
            _cts.Cancel();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

    }
}
