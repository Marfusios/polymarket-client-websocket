using Newtonsoft.Json;
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
        public string Side { get; set; }

        /// <summary>
        /// Order owner.
        /// </summary>
        [JsonProperty("order_owner")]
        public string OrderOwner { get; set; }

        /// <summary>
        /// Original size.
        /// </summary>
        [JsonProperty("original_size")]
        public string OriginalSize { get; set; }

        /// <summary>
        /// Matched size.
        /// </summary>
        [JsonProperty("size_matched")]
        public string SizeMatched { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

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
        public string Type { get; set; }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Expiration timestamp.
        /// </summary>
        [JsonProperty("expiration")]
        public string Expiration { get; set; }

        /// <summary>
        /// Order type.
        /// </summary>
        [JsonProperty("order_type")]
        public string OrderType { get; set; }

        /// <summary>
        /// Order status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Maker address.
        /// </summary>
        [JsonProperty("maker_address")]
        public string MakerAddress { get; set; }

        /// <summary>
        /// Event timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
