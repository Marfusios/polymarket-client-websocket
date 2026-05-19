using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Orderbook price level change.
    /// </summary>
    public class PriceChange
    {
        /// <summary>
        /// Asset token ID.
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Side, BUY or SELL.
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Orderbook hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Best bid after the update.
        /// </summary>
        [JsonProperty("best_bid")]
        public string BestBid { get; set; }

        /// <summary>
        /// Best ask after the update.
        /// </summary>
        [JsonProperty("best_ask")]
        public string BestAsk { get; set; }
    }
}
