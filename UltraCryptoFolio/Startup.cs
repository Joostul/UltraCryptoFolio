using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using System.Linq;
using System.Security.Claims;
using UltraCryptoFolio.Repositories;
using UltraCryptoFolio.Services;

namespace UltraCryptoFolio
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
            var storageAccountConnectionString = Configuration.GetValue<string>("StorageAccountConnectionString");
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages();

            // Infra components
            services.AddHttpClient<IApiPriceRepository, ApiPriceRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAzureStorageAccountRepository, AzureStorageAccountRepository>(a =>
                new AzureStorageAccountRepository(storageAccountConnectionString));

            // Services
            services.AddTransient<IPortfolioService, PortfolioService>();
            services.AddTransient<IAccountService, AccountService>();

            // Repositories
            services.AddTransient<IPortfolioRepository, StoragePortfolioRepository>();
            services.AddTransient<IUserRepository, StorageUserRepository>();
            services.AddTransient<IPriceRepository, StoragePriceRepository>();
            //services.AddTransient<IApiPriceRepository, ApiPriceRepository>();

            // Authentication & Authorisation
            var registeredUserPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireClaim(ClaimTypes.Email)
                .Build();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthorization(options => 
            {
                options.AddPolicy("RegisteredUser", registeredUserPolicy);
            });

            // Add mcv
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
