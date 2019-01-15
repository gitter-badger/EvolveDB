using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB.Cli.Commands
{
    public interface ICommand
    {
        int Run();
    }
}
