
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using WebApiProxy.Core.Models;

namespace WebApiProxy.CommandLine
{
    public class Generator
    {
        private static readonly JsonSerializerSettings JSON_SERIALIZER_SETTINGS =
            new JsonSerializerSettings()
            {
                ConstructorHandling = ConstructorHandling.Default,
                DefaultValueHandling = DefaultValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,

                Converters =
                    new List<JsonConverter>()
                    {
                        new IsoDateTimeConverter()
                    },
            };

        internal static void Generate(Arguments arguments)
        {
            Tasks.Models.Configuration config = new Tasks.Models.Configuration();
            config.Endpoint = arguments.Endpoint;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Proxy-Type", "metadata");
                HttpResponseMessage result = httpClient.GetAsync(config.Endpoint).Result;
                result.EnsureSuccessStatusCode();

                var content = result.Content.ReadAsStringAsync().Result;
                config.Metadata = JsonConvert.DeserializeObject<Metadata>(content, JSON_SERIALIZER_SETTINGS);
            }

            var template = new Tasks.Templates.CSharpProxyTemplate(config);
            var code = template.TransformText();

            File.WriteAllText(arguments.OutputFileName, code);
        }
    }
}
