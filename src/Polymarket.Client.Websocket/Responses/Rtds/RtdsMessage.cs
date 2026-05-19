using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Polymarket.Client.Websocket.Responses.Rtds
{
    /// <summary>
    /// Generic RTDS message.
    /// </summary>
    public class RtdsMessage
    {
        /// <summary>
        /// Subscription topic.
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Message type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// Event-specific payload.
        /// </summary>
        [JsonProperty("payload")]
        public JToken Payload { get; set; }
    }
}
