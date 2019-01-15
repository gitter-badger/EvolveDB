using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB.Internal
{
    public class EvolveEnvironment : IEvolveEnvironment
    {
        public string ConnectionString { get; set; }
        public List<string> Locations { get; set; }
    }
}
