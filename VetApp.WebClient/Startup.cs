using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VDMJasminka.Application.Common;
using VDMJasminka.Application.Database;
using VDMJasminka.Application.Inventory;
using VDMJasminka.Application.Services;
using VDMJasminka.Core;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Factory;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Core.Strategies;
using VDMJasminka.Data;
using VDMJasminka.Data.Repository;
using VDMJasminka.Data.Services;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace VDMJasminka.WebClient
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
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/AmbulatoryProtocol/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddControllersWithViews().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddRazorRuntimeCompilation();
            var assembly = AppDomain.CurrentDomain.Load("VDMJasminka.Application");
            services.AddMediatR(assembly);
            services.AddDbContext<VDMJasminkaDbContext>();
            services.AddScoped<IMigrationsService, MigrationsService>();
            services.AddScoped<IDatabaseMigrationsService, DatabaseMigrationsService>();
            services.AddTransient<IRepository<Owner>, OwnerRepository>();
            services.AddTransient<IRepository<Supplier>, SupplierRepository>();
            services.AddTransient<IRepository<Species>, SpeciesRepository>();
            services.AddTransient<IRepository<Medicament>, MedicamentRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IRepository<SupplierOrderItem>, ResupplyDetailsRepository>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IVaccinationDateService, VaccinationDateService>();
            services.AddTransient<IPdfService, PdfService>();
            services.AddTransient<IDiagnosisService, DiagnosisService>();
            services.AddSingleton<INextVaccinationDateFactory, NextVaccinationDateFactory>();
            services.AddTransient<ICheckupItemInventoryService, CheckupItemInventoryService>();
            services.AddTransient<IMedicamentInventoryService, MedicamentInventoryService>();
            services.AddTransient<IReadOnlyRepository<MedicamentInventory>, InventoryRepository>();



            services.Scan(b => b.FromAssemblies(typeof(INextVaccinationDateStrategy).Assembly)
               .AddClasses(c => c.AssignableTo<INextVaccinationDateStrategy>())
               .AsImplementedInterfaces()
               .WithSingletonLifetime());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}