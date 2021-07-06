using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });
    }

    public class Worker : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private string _serviceBusConnectionString;

        public Worker(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceBusConnectionString = configuration.GetConnectionString("ServiceBusConnectionString");
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var client = new ServiceBusClient(_serviceBusConnectionString);
            var sender = client.CreateSender("cumber");
            await sender.SendMessageAsync(new ServiceBusMessage($"This is a message: {DateTime.UtcNow.ToString()}"));

            Console.WriteLine("Message sent");

            await sender.DisposeAsync();
            await client.DisposeAsync();

            _hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
