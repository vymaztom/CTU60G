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
using CTU60GLib.Client;

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
                CTUClient client = new CTUClient();
                await client.LoginAsync(_workerOptions.CTULogin,_workerOptions.CTUPass);

                await Task.Delay(1);
                break;
            }
        }
    }
}
