using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EvolveDB.Cli.Commands
{
    public class RootCommand : ICommand
    {
        private readonly CommandLineApplication _app;

        public RootCommand(CommandLineApplication app)
        {
            _app = app;
        }

        public static void Configure(CommandLineApplication app)
        {
            app.FullName = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyProductAttribute>()
                .Product;
            app.Name = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyTitleAttribute>()
                .Title;

            app.HelpOption("-?|-h|--help");

            app.VersionOption("-v|--version", Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion);

            app.Command("migrate", MigrateCommand.Configure, false);

            app.OnExecute(() =>
            {
                return (new RootCommand(app)).Run();
            });
        }

        public int Run()
        {
            _app.ShowHelp();
            return 0;
        }

    }
}
