using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VDMJasminka.Application.Common;
using VDMJasminka.Application.Inventory;
using VDMJasminka.Application.Services;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Factory;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Core.Strategies;
using VDMJasminka.Core;
using VDMJasminka.Data.Repository;
using VDMJasminka.Data.Services;
using VDMJasminka.Data;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using VDMJasminka.WebApi.Hubs;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.Core.Ambulance.ACL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using VDMJasminka.Application.Auth;
using VDMJasminka.Application.Users;

namespace VDMJasminka.WebApi
{
    public class Startup
    { 
        private const string API_VERSION = "v1";
        private const string AllowAllCors = "AllowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:SecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddSignalR(opt =>
            {
                opt.HandshakeTimeout = TimeSpan.FromSeconds(60);
                opt.KeepAliveInterval = TimeSpan.FromSeconds(12);
            });

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/AmbulatoryProtocol/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            var assemblies = new Assembly[]
            {
                typeof(Startup).Assembly,
                typeof(VDMJasminkaDbContext).Assembly,
                AppDomain.CurrentDomain.Load("VDMJasminka.Application")
            };
            services.AddMediatR(assemblies);
            services.AddTransient<IRepository<Owner>, OwnerRepository>();
            services.AddTransient<IRepository<Supplier>, SupplierRepository>();
            services.AddTransient<IRepository<Species>, SpeciesRepository>();
            services.AddTransient<IRepository<Medicament>, MedicamentRepository>();
            services.AddTransient<IMedicamentCurrentInventoryAmountService, MedicamentCurrentInventoryAmountService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IInventoryCheck, InventoryCheck>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IVaccinationDateService, VaccinationDateService>();
            services.AddTransient<IPdfService, PdfService>();
            services.AddTransient<IDiagnosisService, DiagnosisService>();
            services.AddSingleton<INextVaccinationDateFactory, NextVaccinationDateFactory>();
            services.AddTransient<ICheckupItemInventoryService, CheckupItemInventoryService>();
            services.AddTransient<IMedicamentInventoryService, MedicamentInventoryService>();
            services.AddTransient<IReadOnlyRepository<MedicamentInventory>, InventoryRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<RefreshTokenSetterAttribute>();
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<DapperContext>();
            services.AddCors(options =>
                {
                    options.AddPolicy(AllowAllCors,
                                      builder =>
                                      {
                                          builder.AllowAnyHeader();
                                          builder.AllowAnyMethod();
                                          builder.AllowAnyOrigin();
                                      });
                });
            services.AddDbContext<VDMJasminkaDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DVMJasminka"))
                .UseSnakeCaseNamingConvention());

            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DVMJasminka")).UseSnakeCaseNamingConvention();
            });

            services.Scan(b => b.FromAssemblies(typeof(INextVaccinationDateStrategy).Assembly)
               .AddClasses(c => c.AssignableTo<INextVaccinationDateStrategy>())
               .AsImplementedInterfaces()
               .WithSingletonLifetime());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DVM-Jasminka.API", Version = API_VERSION });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();

                // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
                c.OperationFilter<SetOperationIdOperationFilter>();
                c.ParameterFilter<ValidCheckupTypeFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.EnableDeepLinking();
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCors(AllowAllCors);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CheckupHub>("/Checkup");
            });
        }
    }
}