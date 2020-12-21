using GridShared;
using JINIApp.Client.Services;
using JINIApp.Shared.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JINIApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ICrudDataService<Item>, ItemService>();
            builder.Services.AddScoped<ICrudDataService<Customer>, CustomerService>();
            builder.Services.AddScoped<ICrudDataService<SalesOrder>, SalesOrderService>();
            builder.Services.AddScoped<ICrudDataService<SalesOrderItem>, SalesOrderItemService>();
            builder.Services.AddScoped<ICrudDataService<Revenue>, RevenueService>();


            await builder.Build().RunAsync();
        }
    }
}
