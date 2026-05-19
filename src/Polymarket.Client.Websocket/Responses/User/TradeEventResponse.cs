using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.User
{
    /// <summary>
    /// User-channel trade event.
    /// </summary>
    public class TradeEventResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Trade type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Trade ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Taker order ID.
        /// </summary>
        [JsonProperty("taker_order_id")]
        public string TakerOrderId { get; set; }

        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Asset token ID.
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        /// <summary>
        /// Side, BUY or SELL.
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Fee rate in bps.
        /// </summary>
        [JsonProperty("fee_rate_bps")]
        public string FeeRateBps { get; set; }

        /// <summary>
        /// Trade status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Match time.
        /// </summary>
        [JsonProperty("matchtime")]
        public string MatchTime { get; set; }

        /// <summary>
        /// Last update timestamp.
        /// </summary>
        [JsonProperty("last_update")]
        public string LastUpdate { get; set; }

        /// <summary>
        /// Outcome label.
        /// </summary>
        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Owner identifier.
        /// </summary>
        [JsonProperty("owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Trade owner.
        /// </summary>
        [JsonProperty("trade_owner")]
        public string TradeOwner { get; set; }

        /// <summary>
        /// Maker address.
        /// </summary>
        [JsonProperty("maker_address")]
        public string MakerAddress { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        /// <summary>
        /// Bucket index.
        /// </summary>
        [JsonProperty("bucket_index")]
        public int? BucketIndex { get; set; }

        /// <summary>
        /// Maker orders.
        /// </summary>
        [JsonProperty("maker_orders")]
        public MakerOrder[] MakerOrders { get; set; }

        /// <summary>
        /// Trader side.
        /// </summary>
        [JsonProperty("trader_side")]
        public string TraderSide { get; set; }

        /// <summary>
        /// Event timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
