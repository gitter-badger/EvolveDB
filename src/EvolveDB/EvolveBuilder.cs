using EvolveDB.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB
{
    public class EvolveBuilder : IEvolveBuilder
    {
        private readonly IList<Action<EvolveBuilderContext, IConfigurationBuilder>> _configureEvolveActions = new List<Action<EvolveBuilderContext, IConfigurationBuilder>>();
        private readonly IList<Action<IConfigurationBuilder>> _configurePreActions = new List<Action<IConfigurationBuilder>>();
        private readonly IList<Action<EvolveBuilderContext, IServiceCollection>> _configureServicesActions = new List<Action<EvolveBuilderContext, IServiceCollection>>();    
        private bool _built = false;
        private EvolveBuilderContext _context;
        private IEvolveEnvironment _environment;
        private IConfiguration _evolveConfiguration;
        private IConfiguration _preConfiguration;
        private IServiceProvider _evolveServices;

        public IEvolve Build()
        {
            if(_built)
            {
                throw new InvalidOperationException();
            }

            _built = true;

            BuildPreConfiguration();
            CreateEnvironment();
            CreateEvolveBuilderContext();
            BuildEvolveConfiguration();
            CreateServiceProvider();
            return _evolveServices.GetRequiredService<IEvolve>();
        }

        /// <summary>
        /// Set up the configuration for the remainder of the build process and evolve.
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public IEvolveBuilder ConfigureEvolveConfiguration(Action<EvolveBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureEvolveActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize the <see cref="IEvolveEnvironment"/>
        /// fro use later in the build process.
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public IEvolveBuilder ConfigurePreConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            _configurePreActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Adds services.
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns></returns>
        public IEvolveBuilder ConfigureServices(Action<EvolveBuilderContext, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }
        private void BuildEvolveConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddConfiguration(_preConfiguration);
            foreach(var buildAction in _configureEvolveActions)
            {
                buildAction(_context, configBuilder);
            }
            _evolveConfiguration = configBuilder.Build();
            _context.Configuration = _evolveConfiguration;

        }
        private void BuildPreConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();

            foreach(var configAction in _configurePreActions)
            {
                configAction(configBuilder);
            }
            _preConfiguration = configBuilder.Build();
        }
        private void CreateEnvironment()
        {
            _environment = new EvolveEnvironment();
        }

        private void CreateEvolveBuilderContext()
        {
            _context = new EvolveBuilderContext()
            {
                Configuration = _preConfiguration,
                Environment = _environment
            };
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_environment);
            services.AddSingleton(_context);
            services.AddSingleton(_evolveConfiguration);
            services.AddSingleton<IEvolve, Internal.Evolve>();
            services.AddOptions();
            services.AddLogging();

            foreach(var configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_context, services);
            }
            _evolveServices = (new DefaultServiceProviderFactory()).CreateServiceProvider(services);

            if(_evolveServices == null)
            {
                throw new InvalidOperationException("IServiceFactoryProvider returned a null IServiceProvider.");
            }
        }
    }
}
