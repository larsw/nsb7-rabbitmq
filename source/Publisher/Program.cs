using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Publisher
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var endpointConfig = new EndpointConfiguration("Publisher");
            var transport = endpointConfig.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");
            endpointConfig.SendOnly();
            var instance = await Endpoint.Start(endpointConfig);

            //instance.Publish<>()
        }
    }
}
