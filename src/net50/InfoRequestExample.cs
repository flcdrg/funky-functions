using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace net50
{
    public class InfoRequestExample : OpenApiExample<InfoRequest>
    {
        public override IOpenApiExample<InfoRequest> Build(NamingStrategy namingStrategy = default!)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("first", "Simple Example", new InfoRequest() { Name = "David", Age = 21 }, namingStrategy));

            return this;
        }
    }
}
