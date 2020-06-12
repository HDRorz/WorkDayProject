using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;

namespace WorkDayDotNetGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        // Set properties and call methods on options
                        serverOptions.AddServerHeader = false;
                        serverOptions.ConfigureEndpointDefaults(listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                            //listenOptions.UseHttps("cacert.p12");
                        });
                        //serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                        //{
                        //    httpsOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                        //});
                    }).UseStartup<Startup>();
                });
    }
}
