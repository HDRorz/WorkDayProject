using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkDayAspCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .Build();

            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            // Set properties and call methods on options
                            serverOptions.AddServerHeader = false;
                            serverOptions.ConfigureEndpointDefaults(listenOptions =>
                            {
                                //listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                //listenOptions.UseHttps("cacert.p12");
                            });
                        })
                        .UseStartup<Startup>();
                    });
    }
}
