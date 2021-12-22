using Appservices.UserServices;
using Appservices.UserServices.Interface;
using AppServices.UserServices.DTO;
using AppServices.UserServices.Validate;
using CRUD_EF.Exceptions;
using CRUD_EF.Migrations;
using EFCore.DbConnection;
using EFCore.Model;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CRUD_EF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //DbContext
            services.AddDbContext<UserContext>(option =>
            {
                option.UseNpgsql(Configuration.GetConnectionString("MyDb"), b => b.MigrationsAssembly("CRUD_EF"));
            });

            //DI            
            services.AddTransient<IUserService<User>, UserService>();

            services.AddTransient<UserLoginService, UserLoginService>();

            services.AddInfrastructureServices();                       

            //DI  Validator
            services.AddTransient<IValidator<AddUserRequestDto>, AddUserValidator>();
            services.AddTransient<IValidator<EditUserRepuestDto >, EditUserValidator>();

            //Validation
            services.AddControllers().AddFluentValidation();

            //Authen

            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKyBytes = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKyBytes),

                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
