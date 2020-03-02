using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Migrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfigFromSettings();
            var serviceProvider = CreateServices(configuration);

            try
            {
                var (type, register) = GetTypeAndRegisterFromArgs(args);
                MigrateByParameters(serviceProvider, type, register);
            }
            catch (ApplicationException ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        #region .:Private methods:.

        #region .:App methods:.

        private static void ShowErrorMessage(string message)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }
        private static IConfigurationRoot GetConfigFromSettings()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }
        private static IServiceProvider CreateServices(IConfiguration configuration)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }


        #endregion

        #region .:Migrate methods:.

        private static void MigrateByParameters(IServiceProvider serviceProvider, string type, long? register)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                if (type == "up" && register == null)
                    UpdateDatabase(runner);
                else if (type == "up" && register != null)
                    UpdateDatabaseByRegeister(runner, (long)register);
                else if (type == "down" && register != null)
                    DowgradeDatabaseByRegister(runner, (long)register);
                else
                    Console.WriteLine("Invalid parameters was informed");
            }
        }
        private static (string, long?) GetTypeAndRegisterFromArgs(string[] args)
        {
            var type = "up";
            var register = (long?)null;

            if (args.Length > 2)
                throw new ApplicationException("You can not inform more the two parameters");

            if (args.Length >= 1)
            {
                if (args.Length == 1 && args[0].ToLower() == "down")
                    throw new ApplicationException("You must inform the register that you want to roll back");
                if (!"up".Equals(args[0]) && !"down".Equals(args[0]))
                    throw new ApplicationException(@"The first parameter must be ""up"" or ""down""");
                type = args[0].ToLower();
            }

            if (args.Length == 2)
            {
                long reg;

                if (!Int64.TryParse(args[1], out reg))
                    throw new ApplicationException(@"The second parameter must be a number of the type long");

                register = reg;
            }

            return (type, register);
        }
        private static void UpdateDatabase(IMigrationRunner runner) => runner.MigrateUp();
        private static void UpdateDatabaseByRegeister(IMigrationRunner runner, long register) => runner.MigrateUp(register);
        private static void DowgradeDatabaseByRegister(IMigrationRunner runner, long register) => runner.MigrateDown(register);

        #endregion

        #endregion
    }
}

