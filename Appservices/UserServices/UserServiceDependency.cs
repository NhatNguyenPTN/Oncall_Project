using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserServices
{
    public static class UserServiceDependency
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
