using Newtonsoft.Json;
using Polymarket.Client.Websocket.Enums;

namespace Polymarket.Client.Websocket.Responses.User
{
    /// <summary>
    /// Maker-side order included in a user trade event.
    /// </summary>
    public class MakerOrder
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Owner identifier.
        /// </summary>
        [JsonProperty("owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Maker address.
        /// </summary>
        [JsonProperty("maker_address")]
        public string MakerAddress { get; set; }

        /// <summary>
        /// Matched amount.
        /// </summary>
        [JsonProperty("matched_amount")]
        public decimal MatchedAmount { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Fee rate in bps.
        /// </summary>
        [JsonProperty("fee_rate_bps")]
        public decimal FeeRateBps { get; set; }

        /// <summary>
        /// Asset token ID.
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        /// <summary>
        /// Outcome label.
        /// </summary>
        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Side, BUY or SELL.
        /// </summary>
        [JsonProperty("side")]
        public PolymarketOrderSide Side { get; set; }
    }
}
