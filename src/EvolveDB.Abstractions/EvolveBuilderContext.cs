using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public class EvolveBuilderContext
    {
        public IConfiguration Configuration { get; set; }

        public IEvolveEnvironment Environment { get; set; }
    }
}
