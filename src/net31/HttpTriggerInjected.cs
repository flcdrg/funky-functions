using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        class Weather
        {
            public string Title { get; set; }
        }

        [FunctionName("HttpTriggerInjected")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _log.LogInformation("Running HttpTriggerInjected");

            var response = await _httpClient.GetAsync("https://gardiner-weather.azurewebsites.net/api/forecast/SA/Adelaide?json=true");

            var contentStream = await response.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();

            var weather = serializer.Deserialize<Weather>(jsonReader);

            _log.LogInformation(weather.Title);

            // Code that depends on Newtonsoft.Json 13
            //const string RegexBacktrackingPattern = "(?<a>(.*?))[|].*(?<b>(.*?))[|].*(?<c>(.*?))[|].*(?<d>[1-3])[|].*(?<e>(.*?))[|].*[|].*[|].*(?<f>(.*?))[|].*[|].*(?<g>(.*?))[|].*(?<h>(.*))";
            //var regexBacktrackingData = new JArray();
            //regexBacktrackingData.Add(new JObject(new JProperty("b", @"15/04/2020 8:18:03 PM|1|System.String[]|3|Libero eligendi magnam ut inventore.. Quaerat et sit voluptatibus repellendus blanditiis aliquam ut.. Quidem qui ut sint in ex et tempore.|||.\iste.cpp||46018|-1")));
            //regexBacktrackingData.SelectTokens(
            //        $"[?(@.b =~ /{RegexBacktrackingPattern}/)]",
            //        new JsonSelectSettings
            //        {
            //            RegexMatchTimeout = TimeSpan.FromSeconds(10.01)
            //        }).ToArray();

            return new OkObjectResult($"Weather title: {weather.Title}");
        }
    }
}
