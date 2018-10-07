using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using BaseHost;

namespace BlueService
{
    public class Blue : BusService
    {
        private static Task Main(string[] args)
        {
            return BusHost<Blue>.Run(args);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            base.StopAsync(cancellationToken);
        }
    }
}
