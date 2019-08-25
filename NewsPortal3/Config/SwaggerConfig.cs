using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NewsPortal3.Config
{
    public class SwaggerConfig
    {
        private static string Name => "My Cool API";
        private static string Version => "v1";
        private static string Endpoint => $"/swagger/{Version}/swagger.json";
        private static string UIEndpoint => "swagger";

        public static void SwaggerUIConfig(SwaggerUIOptions config)
        {
            config.RoutePrefix = UIEndpoint;
            config.SwaggerEndpoint(Endpoint, Name);
        }

        public static void SwaggerGenConfig(SwaggerGenOptions config)
        {
            config.SwaggerDoc(
                Version,
                new OpenApiInfo { Version = Version, Title = Name });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
        }
    }
}
