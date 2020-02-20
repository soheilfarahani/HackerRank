using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var validJobNumbers = new[] { 1, 2 };

            var consoleServiceFactory = serviceProvider.GetService<IConsoleServiceFactory>();
            System.Console.WriteLine("Please enter the service Number you want to run: \n\n[1] Queue Item Migration\n[2] Auth User data migration");
            var jobNumberString = System.Console.ReadLine().ToString();
            if (!int.TryParse(jobNumberString, out int jobNumber))
            {
                System.Console.WriteLine("\nPlease type valid number.\n");
                return;
            }
            if (!validJobNumbers.Contains(jobNumber))
            {
                System.Console.WriteLine("\nPlease type the number that is available and has associated to a job as shown.");
                return;
            }

            var consoleService = jobNumber switch
            {
                1 => consoleServiceFactory.GetService(ServiceKey.HackerRankRunner),
                _ => null
            };

            if (consoleService == null)
            {
                System.Console.WriteLine("Sorry could not find any Service to run!");
                return;
            }

            System.Console.WriteLine($"You are about to start Console Service '{ServiceKey.HackerRankRunner.ToString()}', to continue press c or e to exit.\n");
            var key = System.Console.ReadKey().KeyChar.ToString();
            if (!key.ToLower().Equals("c"))
            {
                return;
            }

            await consoleService.Run();
            System.Console.ReadKey();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddOptions();
            services.AddLogging();
            services.AddSingleton<IConsoleServiceFactory, ConsoleServiceFactory>();
         

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
