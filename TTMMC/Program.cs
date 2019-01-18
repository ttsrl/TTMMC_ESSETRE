using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTMMC.Services;

namespace TTMMC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
#if DEBUG
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DBContext>();
                    DBContext.Initialize(context);
                }
                catch (Exception ex)
                {

                }
            }
#endif
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
