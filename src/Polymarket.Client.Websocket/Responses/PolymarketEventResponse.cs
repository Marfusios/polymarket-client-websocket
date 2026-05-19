using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polymarket.Client.Websocket.Enums;

namespace Polymarket.Client.Websocket.Responses
{
    /// <summary>
    /// Base type for Polymarket event_type messages.
    /// </summary>
    public abstract class PolymarketEventResponse
    {
        /// <summary>
        /// Event type.
        /// </summary>
        [JsonProperty("event_type")]
        public PolymarketEventType EventType { get; set; }

        /// <summary>
        /// Unknown fields preserved for forward compatibility.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; } = new Dictionary<string, JToken>();
    }
}
