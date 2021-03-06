using System.IO;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace net50
{
    public static class HttpTrigger
    {
        [Function("HttpTrigger")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpTrigger");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            using var sr = new StreamReader(req.Body);
            using var reader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();
            var info = serializer.Deserialize<InfoRequest>(reader);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions - .NET 5 isolated! {info.Name}");

            return response;
        }
    }
}
