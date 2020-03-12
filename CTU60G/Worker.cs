using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CTU60G.Json;
using Newtonsoft.Json.Linq;

namespace CTU60G
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions _workerOptions;

        public Worker(ILogger<Worker> logger, WorkerOptions workerOptions)
        {
            _logger = logger;
            _workerOptions = workerOptions;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string json = "";
                using (WebClient wc = new System.Net.WebClient())
                {
                    json = wc.DownloadString("http://is.sychrovnet.cz/public/60g_ctu_api.php?key=sdf82Jd92msmn9jdlnf0ejS&action=getDevices&unreg=1&limit=5");
                }


                var jObj = JObject.Parse(json).Children().Children();
                List<WirelessSite> sites = new List<WirelessSite>();
                foreach (var site in jObj)
                {
                    sites.Add(site.ToObject<WirelessSite>());
                }

                CTUWebManagement web = new CTUWebManagement(_workerOptions);

                    web.LogIn();
                await Task.Delay(1);
                break;
            }
        }
    }
}
