using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Api.Tests.Utils
{
    internal static class JsonOptions
    {
        public static JsonSerializerOptions ReturnOptions()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return options;
        }
    }
}
