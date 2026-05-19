using Newtonsoft.Json;
using Polymarket.Client.Websocket.Enums;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.User
{
    /// <summary>
    /// User-channel order event.
    /// </summary>
    public class OrderEventResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Owner identifier.
        /// </summary>
        [JsonProperty("owner")]
        public string Owner { get; set; }

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
        /// Order owner.
        /// </summary>
        [JsonProperty("order_owner")]
        public string OrderOwner { get; set; }

        /// <summary>
        /// Original size.
        /// </summary>
        [JsonProperty("original_size")]
        public decimal OriginalSize { get; set; }

        /// <summary>
        /// Matched size.
        /// </summary>
        [JsonProperty("size_matched")]
        public decimal SizeMatched { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Associated trades, if present.
        /// </summary>
        [JsonProperty("associate_trades")]
        public string[] AssociateTrades { get; set; }

        /// <summary>
        /// Outcome label.
        /// </summary>
        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Order event type.
        /// </summary>
        [JsonProperty("type")]
        public PolymarketOrderUpdateType Type { get; set; }

        /// <summary>
        /// Creation Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Expiration Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("expiration")]
        public long Expiration { get; set; }

        /// <summary>
        /// Order type.
        /// </summary>
        [JsonProperty("order_type")]
        public PolymarketTimeInForce OrderType { get; set; }

        /// <summary>
        /// Order status.
        /// </summary>
        [JsonProperty("status")]
        public PolymarketOrderStatus Status { get; set; }

        /// <summary>
        /// Maker address.
        /// </summary>
        [JsonProperty("maker_address")]
        public string MakerAddress { get; set; }

        /// <summary>
        /// Event Unix timestamp in seconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
