using System;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyFridgeListWebapi.Application.Account.Commands.SignUp;
using MyFridgeListWebapi.Core.Behaviours;
using MyFridgeListWebapi.Core.Data.Database;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Environment;
using MyFridgeListWebapi.Core.Services;
using MyFridgeListWebapi.Extensions;
using MyFridgeListWebapi.Services;
using Swashbuckle.AspNetCore.Filters;

namespace MyFridgeListWebapi
{
    public sealed class Startup
    {
        private AppConfiguration _appConfiguration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public AppConfiguration AppConfiguration
        {
            get
            {
                if (_appConfiguration == null)
                {
                    _appConfiguration = new AppConfiguration();
                    Configuration
                        .Bind(_appConfiguration);
                }

                return _appConfiguration;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfiguration>(Configuration);
            services.AddSingleton(AppConfiguration);
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(SignUpCommand).GetTypeInfo().Assembly);
            services.AddScoped<IJwTokenService, JwtTokenService>();
            services.AddDbContextPool<DatabaseContext>(
                options =>
                {
                    options.UseMySql(
                        AppConfiguration.DefaultConnectionString,
                        sqlServerOptions => sqlServerOptions.CommandTimeout(
                            AppConfiguration.DatabaseConfiguration.CommandTimeout));
                },
                AppConfiguration.DatabaseConfiguration.DbContextPoolSize);

            services.AddIdentity<User, IdentityRole<Guid>>(config =>
            {
                config.SignIn.RequireConfirmedEmail = AppConfiguration.AccountConfiguration.RequireConfirmedEmail;
                config.User.RequireUniqueEmail = AppConfiguration.AccountConfiguration.RequireUniqueEmail;
                config.Password.RequireDigit = AppConfiguration.AccountConfiguration.PasswordRequireDigit;
                config.Password.RequireLowercase = AppConfiguration.AccountConfiguration.PasswordRequireLowercase;
                config.Password.RequireUppercase = AppConfiguration.AccountConfiguration.PasswordRequireUppercase;
                config.Password.RequireNonAlphanumeric = AppConfiguration.AccountConfiguration.PasswordRequireNonAlphanumeric;
                config.Password.RequiredLength = AppConfiguration.AccountConfiguration.PasswordRequiredLength;
            })
            .AddErrorDescriber<Core.Localizations.IdentityErrorDescriber>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            services.AddJwtAuthentication(
                new TokenValidationParameters
                {
                    ValidIssuer = AppConfiguration.JwtConfiguration.Issuer,
                    ValidAudience = AppConfiguration.JwtConfiguration.Issuer,
                    ValidateLifetime = AppConfiguration.JwtConfiguration.ValidateLifetime,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JwtConfiguration.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                },
                AppConfiguration.JwtConfiguration.IncludeErrorDetails);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilterBehaviour));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpCommandValidator>())
            .AddMvcOptions(options =>
            {
                options.ModelMetadataDetailsProviders.Clear();
                options.ModelValidatorProviders.Clear();
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            if (!Environment.IsProduction())
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFridgeList Webapi", Version = "v1" });
                    options.CustomSchemaIds(x => x.FullName);
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = string.Format("Standard Authorization header using the Bearer scheme. Example: \"Authorization: {0} {1}\"", JwtBearerDefaults.AuthenticationScheme, "{token}"),
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFridgeList Webapi v1"));
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
