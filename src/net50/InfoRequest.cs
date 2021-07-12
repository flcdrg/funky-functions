using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace net50
{
    [OpenApiExample(typeof(InfoRequestExample))]
    public class InfoRequest
    {
        [OpenApiProperty(Description = "A name of something", Nullable = false)]
        [MaxLength(10)]
        public string? Name { get; set; }

        [OpenApiProperty(Description = "How old", Default = 0)]
        [Range(0, 100)]
        public int Age { get; set; }

        [OpenApiProperty(Nullable = true)]
        public string? NullableString { get; set; }

    }
}
