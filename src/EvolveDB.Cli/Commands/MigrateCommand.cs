using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB.Cli.Commands
{
    public class MigrateCommand : ICommand
    {
        private readonly CommandLineApplication _app;

        public MigrateCommand(CommandLineApplication app)
        {
            _app = app;
        }
        public static void Configure(CommandLineApplication app)
        {
            app.OnExecute(() => new MigrateCommand(app).Run());
        }

        public int Run()
        {
            var evolve = new EvolveBuilder()
                .Build();
            return 0;
        }
    }
}
