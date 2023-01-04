using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RICH_Connector.API.ApiStartup;
using System.Threading;

namespace RICH_Connector.API
{
    public class Server
    {
        private IWebHost? server = null;

        public void Restart()
        {
            this.server = this.buildWebHost();
            Task.Run(() =>
            {
                this.server.Run();
            });
        }
        private IWebHost buildWebHost()
        {
            WebHost.CreateDefaultBuilder();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ApiStartup>()
                .Build();

            return host;
        }
    }
}
