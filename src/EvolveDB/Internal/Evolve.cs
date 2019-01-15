using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB.Internal
{
    internal class Evolve : IEvolve
    {
        private readonly ILogger<Evolve> _logger;
        public Evolve(IServiceProvider services, ILogger<Evolve> logger)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IServiceProvider Services { get;}

        public void Dispose()
        {
            (Services as IDisposable)?.Dispose();
        }
    }
}
