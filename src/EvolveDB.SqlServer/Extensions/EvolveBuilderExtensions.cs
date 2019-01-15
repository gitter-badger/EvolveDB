using EvolveDB.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvolveDB.SqlServer.Extensions
{
    public static class EvolveBuilderExtensions
    {
        public static void UseSqlServer(this IEvolveBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IDriver, SqlServerDriver>();
            });

        }
    }
}
