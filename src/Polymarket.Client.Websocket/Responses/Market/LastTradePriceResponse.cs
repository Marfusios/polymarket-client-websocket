using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Last trade price event.
    /// </summary>
    public class LastTradePriceResponse : PolymarketEventResponse
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
        /// Trade price.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Trade size.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Fee rate in bps.
        /// </summary>
        [JsonProperty("fee_rate_bps")]
        public string FeeRateBps { get; set; }

        /// <summary>
        /// Side, BUY or SELL.
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }
    }
}
