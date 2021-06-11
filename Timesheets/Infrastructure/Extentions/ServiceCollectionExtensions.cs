using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Timesheets.Data;
using Timesheets.Data.Implementation;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Implementation;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto.Auth;

namespace Timesheets.Infrastructure.Extentions
{
    internal static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TimesheetDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    b => b.MigrationsAssembly("Timesheets"));
            });
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtAccessOptions>(configuration.GetSection("Authentication:JwtAccessOptions"));
            var jwtAccessSettings = new JwtOptions();
            configuration.Bind("Authentication:JwtAccessOptions", jwtAccessSettings);

            services.Configure<JwtRefreshOptions>(configuration.GetSection("Authentication:JwtRefreshOptions"));
            var jwtRefreshSettings = new JwtOptions();
            configuration.Bind("Authentication:JwtRefreshOptions", jwtRefreshSettings);

            services.AddTransient<ILoginManager, LoginManager>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = jwtAccessSettings.GetTokenValidationParameters();
            });

        }
        public static void ConfigureDomainManagers(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ISheetManager, SheetManager>();
            services.AddScoped<IContractManager, ContractManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<ILoginManager, LoginManager>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ISheetRepo, SheetRepo>();
            services.AddScoped<IContractRepo, ContractRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<ITokenRepo, TokenRepo>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheets", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference(){Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                    },
                    Array.Empty<string>()
                    }
                });
            });
        }
    }
}