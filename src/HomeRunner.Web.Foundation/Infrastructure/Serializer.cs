
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SlidingApps.Collaboration.Web.Infrastructure
{
    public static class Serializer
    {
        /// <summary>
        /// Default JSON.Net serializer settings.
        /// </summary>
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

        public static string ToJson(this object instance)
        {
            string serialized = JsonConvert.SerializeObject(instance, Serializer.JSON_SERIALIZER_SETTINGS);

            return serialized;
        }

        public static T FromJson<T>(this string json)
        {
            T instance = JsonConvert.DeserializeObject<T>(json, JSON_SERIALIZER_SETTINGS);

            return instance;
        }
    }
}
