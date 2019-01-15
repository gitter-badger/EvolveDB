using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public static class EvolveBuilderExtensions
    {
        /// <summary>
        /// Adds services.
        /// </summary>
        /// <param name="evolveBuilder"></param>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public static IEvolveBuilder ConfigureServices(this IEvolveBuilder evolveBuilder, Action<IServiceCollection> configureDelegate)
        {
            return evolveBuilder.ConfigureServices((context, collection) => configureDelegate(collection));
        }
    }
}
