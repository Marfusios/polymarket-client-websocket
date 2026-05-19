using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Market tick size update event.
    /// </summary>
    public class TickSizeChangeResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Asset token ID.
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Previous tick size.
        /// </summary>
        [JsonProperty("old_tick_size")]
        public string OldTickSize { get; set; }

        /// <summary>
        /// New tick size.
        /// </summary>
        [JsonProperty("new_tick_size")]
        public string NewTickSize { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
