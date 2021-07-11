using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace net31
{
    public class HttpTriggerInjected
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpTriggerInjected> _log;

        public HttpTriggerInjected(HttpClient httpClient, ILogger<HttpTriggerInjected> log)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        [FunctionName("HttpTriggerInjected")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _log.LogInformation("Running HttpTriggerInjected");

            var response = await _httpClient.GetAsync("https://david.gardiner.net.au");

            return new OkObjectResult($"Got request {req.Path} and response {response.StatusCode}");
        }
    }
}
