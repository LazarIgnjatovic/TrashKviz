using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // .ConfigureServices((context, services) =>
            // {
            //     HostConfig.CertPath = context.Configuration["CertPath"];
            //     HostConfig.CertPassword = context.Configuration["certpassword"];              
            //     HostConfig.HostDnsEntry = context.Configuration["HostDnsEntry"];
            // })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // webBuilder.ConfigureKestrel(opcije =>
                // {
                //     var host = Dns.GetHostEntry(HostConfig.HostDnsEntry);
                //     opcije.Listen(host.AddressList[0], 5000);
                //     opcije.Listen(host.AddressList[0], 5001, listenOpcije =>
                //     {
                //         listenOpcije.UseHttps(HostConfig.CertPath, HostConfig.CertPassword);
                //     });
                // });
                webBuilder.UseStartup<Startup>();
            });
    }

    public static class HostConfig
    {
        public static string CertPath { get; set; }
        public static string CertPassword { get; set; }
        public static int MaxRequestBodySize { get; set; }
        public static string HostDnsEntry { get; set; }
    }
}
