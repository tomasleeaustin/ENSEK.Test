using AutoMapper;
using ENSEK.Api.Models;
using ENSEK.Api.Services;
using ENSEK.Api.Services.Interfaces;
using ENSEK.Api.Validators;
using ENSEK.Data.Access;
using ENSEK.Data.Access.DbContexts;
using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Data.Access.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENSEK.Api
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
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddControllers()
                .AddFluentValidation();

            services.AddTransient<IEnsekDbContext, EnsekDbContext>(
                options => new EnsekDbContext(connectionStrings.EnsekDb));

            services.AddTransient<IMeterReadingService, MeterReadingService>(
                options => new MeterReadingService(
                    options.GetRequiredService<IEnsekDbContext>(),
                    options.GetRequiredService<IMapper>()));

            services.AddTransient<IValidator<MeterReadingDto>, MeterReadingValidator>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
