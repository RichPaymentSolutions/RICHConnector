using Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Internal;
using Newtonsoft.Json.Serialization;
using RICH_Connector.API.Filter;

namespace RICH_Connector.API
{
    public class ApiStartup
    {
        public ApiStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore((options) =>
            {
                options.Filters.Add<ExceptionFilter>();
            }).AddJsonFormatters()
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }


        public void Configure(IApplicationBuilder app)
        {
            //app.Use((ctx, next) =>
            //{
            //    ctx.Response.Headers.Add("Access-Control-Allow-Origin", ctx.Request.Headers["Origin"]);
            //    ctx.Response.Headers.Add("Access-Control-Allow-Methods", "*");
            //    ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            //    ctx.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            //    ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");

            //    if (ctx.Request.Method.ToLower() == "options")
            //    {
            //        ctx.Response.StatusCode = 204;

            //        return Task.CompletedTask;
            //    }
            //    return next();
            //});
            app.UseMvc();

        }
    }

}
