using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Full aggregated orderbook snapshot for an asset.
    /// </summary>
    public class OrderBookSnapshotResponse : PolymarketEventResponse
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
        /// Bid levels.
        /// </summary>
        [JsonProperty("bids")]
        public OrderBookLevel[] Bids { get; set; }

        /// <summary>
        /// Ask levels.
        /// </summary>
        [JsonProperty("asks")]
        public OrderBookLevel[] Asks { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Orderbook hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
