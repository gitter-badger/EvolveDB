using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace EvolveDB.Tests
{
    public class EvolveBuilderTests
    {
        [Fact]
        public void ConfigureEvolveConfigurationPropagated()
        {
            var evolve = new EvolveBuilder()
                .ConfigurePreConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string,string>("key1", "value1")
                    });
                })
                .ConfigurePreConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("key2", "value2")
                    });
                })
                .ConfigurePreConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("key2", "value3"),
                        new KeyValuePair<string, string>("key3", "value1")
                    });
                })
                .ConfigureEvolveConfiguration((context, configBuilder) =>
                {
                    Assert.Equal("value1", context.Configuration["key1"]);
                    Assert.Equal("value3", context.Configuration["key2"]);
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("key3", "value2")
                    });
                })
                .Build();

            using (evolve)
            {
                var config = evolve.Services.GetRequiredService<IConfiguration>();
                Assert.Equal("value1", config["key1"]);
                Assert.Equal("value3", config["key2"]);
                Assert.Equal("value2", config["key3"]);
            }
        }

        [Fact]
        public void BuildAndDispose()
        {
            using (var evolve = new EvolveBuilder()
                .Build())
            { }
        }

        [Fact]
        public void DefaultServicesAreAvailable()
        {
            using (var evolve = new EvolveBuilder()
                .Build())
            {
                Assert.NotNull(evolve.Services.GetRequiredService<IEvolveEnvironment>());
                Assert.NotNull(evolve.Services.GetRequiredService<IConfiguration>());
                Assert.NotNull(evolve.Services.GetRequiredService<EvolveBuilderContext>());               
            }
        }




        internal class ServiceC
        {
            public ServiceC(ServiceD serviceD) { }
        }

        internal class ServiceD { }
    }
}
