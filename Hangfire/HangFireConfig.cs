using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hangfire
{
    public static class HangFireConfig
    {
        static string ConnectionString = "Host=localhost;Database=useroncall2;Port=5432;User ID=postgres; Username=postgres;Password=224339";

        public static IServiceCollection HangfireConfig(this IServiceCollection services)
        {
            

            return services;
        }
    }
}
