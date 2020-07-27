using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Solve.FinancialMarketIntegration.API.Areas.Security.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SecurityDataContext = Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.DataContext;
using WorkflowDataContext = Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.WorkflowDataContext;
using FilesDataContext = Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.FileDataContext;
using Solve.FinancialMarketIntegration.API.Areas.Files.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

using FluentValidation.AspNetCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache;

/*
    Referências:

    Swagger:
    https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?
         
     
*/

namespace Solve.FinancialMarketIntegration.API
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

            env.EnvironmentName = configuration["Environment"];
        }

        private IHostingEnvironment Environment;

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDataContexts(services);
            ConfigureMVC(services);
            ConfigureSwagger(services);
            ConfigureFilters(services);
            ConfigureSettings(services);
            ConfigureJWTToken(services);
            ConfigureAppServices(services);
            ConfigureEventHandlers(services);
            ConfigureCaches(services);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void ConfigureCaches(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<WorkflowCache>();
        }

        private static void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddScoped<Areas.Tickets.Events.EventBus>();
            services.AddScoped<Areas.WK.Events.EventBus>();

            var eventHandlerTypes = new[] { typeof(Areas.Tickets.Events.IEventHandler<>), typeof(Areas.WK.Events.IEventHandler<>) };

            var eventHandlers = Assembly.GetExecutingAssembly().GetTypes()
                                                                  .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && eventHandlerTypes.Contains(gi.GetGenericTypeDefinition())))
                                 .ToArray();

            foreach (var handler in eventHandlers)
            {
                var interfaces = handler.GetInterfaces().Where(i => i.IsGenericType && eventHandlerTypes.Contains(i.GetGenericTypeDefinition()));
                foreach (var handlerInterface in interfaces)
                {
                    services.AddScoped(handlerInterface, handler);
                }
            }
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            services.AddScoped<IWorkflowService, WorkflowController>();
            services.AddScoped<IAccountManagerService, AccountManagerService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITicketBulkService<TicketVariableIncomeModel>, TicketVariableIncomeController>();
            services.AddScoped(typeof(TicketListQueryService<,>));
            services.AddScoped<IExportService, ExportService>();

            if (Environment.IsDevelopment())
            {
                services.AddScoped<IEmailService, EmailServiceDev>();
            }
            else
            {
                services.AddScoped<IEmailService, EmailService>();
            }
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("Security:EmailSettings"));
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<JWTTokenSettings>(Configuration.GetSection("Security:JWTTokenSettings"));
            services.Configure<PasswordSettings>(Configuration.GetSection("Security:PasswordSettings"));
            services.Configure<UserSettings>(Configuration.GetSection("Security:UserSettings"));
        }

        private static void ConfigureFilters(IServiceCollection services)
        {
            services.AddScoped<RequiredRoleFilter>();
            services.AddScoped<RequiredAccountManagerFilter>();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "Access Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityRequirement(security);
            });
        }

        private static void ConfigureMVC(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CheckTokenFilter));
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                })
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // TODO: Alterar a política de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        private void ConfigureDataContexts(IServiceCollection services)
        {
            services.AddDbContext<SecurityDataContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:SecurityDataContext"]));
            services.AddDbContext<TicketDataContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:TicketsDataContext"]));
            services.AddDbContext<WorkflowDataContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:WorkflowDataContext"]));
            services.AddDbContext<FilesDataContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FilesDataContext"]));
        }

        private void ConfigureJWTToken(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["Security:JWTTokenSettings:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            // configure DI for application services
            // services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISecurityService, SecurityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors("Default");

            app.UseAuthentication();


            #region INICIO Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion FIM Swagger


            app.UseMvc();
        }
    }
}
