namespace BaseHost
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.IO;
    using System.Threading.Tasks;
    using System.Threading;
    using NServiceBus;

    public abstract class BusService : IHostedService
    {
        private EndpointConfiguration _endpointConfiguration;
        public IEndpointInstance Bus { get; private set; }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            _endpointConfiguration = new EndpointConfiguration(GetType().Name);
            var transport = _endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");
            _endpointConfiguration.EnableInstallers();
            Bus = await Endpoint.Start(_endpointConfiguration);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            await Bus.Stop();
        }
    }

    public class BusHost<TService>
        where TService : BusService
    {
        public static async Task Run(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddHostedService<TService>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }
    }
}
