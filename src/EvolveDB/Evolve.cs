using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public static class Evolve
    {
        public static IEvolveBuilder CreateDefaultBuilder()
        {
            var builder = new EvolveBuilder();
            return builder;
        }
    }
}
