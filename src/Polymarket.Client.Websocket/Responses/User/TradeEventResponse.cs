using Newtonsoft.Json;
using Polymarket.Client.Websocket.Enums;
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
        public PolymarketTradeEventType Type { get; set; }

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
        public PolymarketOrderSide Side { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

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
        /// Trade status.
        /// </summary>
        [JsonProperty("status")]
        public PolymarketTradeStatus Status { get; set; }

        /// <summary>
        /// Match Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("matchtime")]
        public long MatchTime { get; set; }

        /// <summary>
        /// Last update Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("last_update")]
        public long LastUpdate { get; set; }

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
        public PolymarketTradeRole TraderSide { get; set; }

        /// <summary>
        /// Event Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
