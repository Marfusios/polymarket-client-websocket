using System;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Responses;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// New market creation event.
    /// </summary>
    public class NewMarketResponse : PolymarketEventResponse
    {
        /// <summary>
        /// Market ID.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Market question.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }

        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Market slug.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Market description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Asset token IDs.
        /// </summary>
        [JsonProperty("assets_ids")]
        public string[] AssetIds { get; set; }

        /// <summary>
        /// Outcome labels.
        /// </summary>
        [JsonProperty("outcomes")]
        public string[] Outcomes { get; set; }

        /// <summary>
        /// Parent event data.
        /// </summary>
        [JsonProperty("event_message")]
        public NewMarketEventMessage EventMessage { get; set; }

        /// <summary>
        /// Unix timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Market tags.
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Condition ID.
        /// </summary>
        [JsonProperty("condition_id")]
        public string ConditionId { get; set; }

        /// <summary>
        /// Whether the market is active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// CLOB token IDs.
        /// </summary>
        [JsonProperty("clob_token_ids")]
        public string[] ClobTokenIds { get; set; }

        /// <summary>
        /// Sports market type, if present.
        /// </summary>
        [JsonProperty("sports_market_type")]
        public string SportsMarketType { get; set; }

        /// <summary>
        /// Sports line, if present.
        /// </summary>
        [JsonProperty("line")]
        public decimal? Line { get; set; }

        /// <summary>
        /// Sports game start time, if present.
        /// </summary>
        [JsonProperty("game_start_time")]
        public DateTime? GameStartTime { get; set; }

        /// <summary>
        /// Minimum tick size.
        /// </summary>
        [JsonProperty("order_price_min_tick_size")]
        public decimal? OrderPriceMinTickSize { get; set; }

        /// <summary>
        /// Group item title.
        /// </summary>
        [JsonProperty("group_item_title")]
        public string GroupItemTitle { get; set; }
    }

    /// <summary>
    /// Parent event data included in a new market event.
    /// </summary>
    public class NewMarketEventMessage
    {
        /// <summary>
        /// Event ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Event ticker.
        /// </summary>
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        /// <summary>
        /// Event slug.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Event title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Event description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
