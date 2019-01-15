using System;

namespace EvolveDB
{
    public interface IEvolve : IDisposable
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        IServiceProvider Services { get; }
    }
}
