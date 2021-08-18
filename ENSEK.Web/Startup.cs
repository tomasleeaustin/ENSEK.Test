using ENSEK.Data.Access;
using ENSEK.Data.Access.DbContexts;
using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Web.Services;
using ENSEK.Web.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ENSEK.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiEndpointAddress = Configuration.GetSection("ApiEndpointAddress").Get<string>();
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddTransient<IEnsekDbContext, EnsekDbContext>(
                options => new EnsekDbContext(connectionStrings.EnsekDb));

            services.AddTransient<IAccountService, AccountService>(
                options => new AccountService(
                    options.GetRequiredService<IEnsekDbContext>(),
                    apiEndpointAddress));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
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
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
