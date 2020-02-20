using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using System.Threading.Tasks;

namespace ConsoleApp
{

    public interface IConsoleService
    {
        Task Run();
    }

    public interface IConsoleServiceFactory
    {
        IConsoleService GetService(ServiceKey serviceKey);
    }

    public class ConsoleServiceFactory : IConsoleServiceFactory
    {
        private readonly ILogger<ConsoleServiceFactory> _logger;

        private readonly IServiceProvider _serviceProvider;
        public ConsoleServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<ConsoleServiceFactory>();
        }

        public IConsoleService GetService(ServiceKey serviceKey)
        {
            switch (serviceKey)
            {
                case ServiceKey.HackerRankRunner:
                    return _serviceProvider.GetService<HackerRankService>();
                default:
                    _logger.LogError($"No console service found with the supplied ServiceKey {serviceKey.ToString()}");
                    throw new ArgumentException($"No console service found with the supplied ServiceKey {serviceKey.ToString()}");
            }
        }
    }
}