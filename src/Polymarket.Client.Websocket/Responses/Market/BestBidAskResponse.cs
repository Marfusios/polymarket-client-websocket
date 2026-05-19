using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Best bid and ask update event.
    /// </summary>
    public class BestBidAskResponse : PolymarketEventResponse
    {
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
        /// Best bid.
        /// </summary>
        [JsonProperty("best_bid")]
        public decimal BestBid { get; set; }

        /// <summary>
        /// Best ask.
        /// </summary>
        [JsonProperty("best_ask")]
        public decimal BestAsk { get; set; }

        /// <summary>
        /// Spread.
        /// </summary>
        [JsonProperty("spread")]
        public decimal Spread { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
