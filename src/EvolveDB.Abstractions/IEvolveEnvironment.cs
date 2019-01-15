using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public interface IEvolveEnvironment
    {
        string ConnectionString { get; set; }

        List<string> Locations { get; set; }
    }
}
