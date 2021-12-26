using Appservices.UserServices;
using Appservices.UserServices.Interface;
using AppServices.UserServices.DTO;
using AppServices.UserServices.Validate;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.UserServices
{
    public static class UserServiceDependency
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<UserLoginService, UserLoginService>();

            //DI  Validator
            services.AddTransient<IValidator<AddUserRequestDto>, AddUserValidator>();
            services.AddTransient<IValidator<EditUserRepuestDto>, EditUserValidator>();

           


            return services;
        }
    }
}
