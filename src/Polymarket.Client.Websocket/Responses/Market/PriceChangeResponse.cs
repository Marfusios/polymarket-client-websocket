using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Orderbook price level update event.
    /// </summary>
    public class PriceChangeResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Price changes.
        /// </summary>
        [JsonProperty("price_changes")]
        public PriceChange[] PriceChanges { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
