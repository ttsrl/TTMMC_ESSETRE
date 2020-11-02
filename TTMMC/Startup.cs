using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TTMMC_ESSETRE.Services;
using Rotativa.AspNetCore;

namespace TTMMC
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
            //database
            services.AddDbContext<TTMMCContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<owlDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OWLConnection")));

            //servizi
            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Utilities>();
            services.AddSingleton<MachinesService>();
            services.AddSingleton<LayoutListener>();
            services.AddSingleton<Barcode>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            RotativaConfiguration.Setup(env);
        }
    }
}
