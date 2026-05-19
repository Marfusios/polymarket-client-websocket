using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Polymarket.Client.Websocket.Json
{
    /// <summary>
    /// Shared JSON serializer settings for Polymarket websocket messages.
    /// </summary>
    public static class PolymarketJsonSerializer
    {
        /// <summary>
        /// Unified JSON settings.
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Converters =
            {
                new PolymarketDecimalConverter(),
                new PolymarketLongConverter(),
                new PolymarketIntConverter(),
                new PolymarketDateTimeConverter()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Custom preconfigured serializer.
        /// </summary>
        public static readonly JsonSerializer Serializer = JsonSerializer.Create(Settings);

        /// <summary>
        /// Serialize an object using Polymarket settings.
        /// </summary>
        public static string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }

        /// <summary>
        /// Deserialize text using Polymarket settings.
        /// </summary>
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, Settings);
        }
    }
}
