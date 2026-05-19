using System;
using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Sports
{
    /// <summary>
    /// Sports result update.
    /// </summary>
    public class SportsResultResponse
    {
        /// <summary>
        /// Game slug.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Whether the game is live.
        /// </summary>
        [JsonProperty("live")]
        public bool Live { get; set; }

        /// <summary>
        /// Whether the game ended.
        /// </summary>
        [JsonProperty("ended")]
        public bool Ended { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        [JsonProperty("score")]
        public string Score { get; set; }

        /// <summary>
        /// Period.
        /// </summary>
        [JsonProperty("period")]
        public string Period { get; set; }

        /// <summary>
        /// Elapsed game time.
        /// </summary>
        [JsonProperty("elapsed")]
        public string Elapsed { get; set; }

        /// <summary>
        /// Last update timestamp.
        /// </summary>
        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }
    }
}
