using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public interface IEvolveBuilder
    {
        IEvolveBuilder ConfigurePreConfiguration(Action<IConfigurationBuilder> configureDelegate);

        IEvolveBuilder ConfigureEvolveConfiguration(Action<EvolveBuilderContext, IConfigurationBuilder> configureDelegate);

        IEvolveBuilder ConfigureServices(Action<EvolveBuilderContext, IServiceCollection> configureDelegate);

        IEvolve Build();
    }
}
