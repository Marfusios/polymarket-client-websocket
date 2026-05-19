using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Market resolution event.
    /// </summary>
    public class MarketResolvedResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Market ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Asset token IDs.
        /// </summary>
        [JsonProperty("assets_ids")]
        public string[] AssetIds { get; set; }

        /// <summary>
        /// Winning asset token ID.
        /// </summary>
        [JsonProperty("winning_asset_id")]
        public string WinningAssetId { get; set; }

        /// <summary>
        /// Winning outcome.
        /// </summary>
        [JsonProperty("winning_outcome")]
        public string WinningOutcome { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Market tags.
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
    }
}
