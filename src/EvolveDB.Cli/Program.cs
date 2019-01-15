using EvolveDB.Cli.Commands;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace EvolveDB.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            RootCommand.Configure(app);
            return app.Execute();
        }
    }
}
