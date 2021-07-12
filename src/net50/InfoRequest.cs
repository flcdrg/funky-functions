using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace net50
{
    [OpenApiExample(typeof(InfoRequestExample))]
    public class InfoRequest
    {
        [OpenApiProperty(Description = "A name of something")]
        [MaxLength(10)]
        public string Name { get; set; }

        [OpenApiProperty(Description = "How old")]
        [Range(0, 100)]
        public int Age { get; set; }
    }
}
