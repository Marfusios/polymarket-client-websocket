using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Rtds
{
    /// <summary>
    /// RTDS crypto or equity price update payload.
    /// </summary>
    public class RtdsPricePayload
    {
        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Price value.
        /// </summary>
        [JsonProperty("value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Full precision value, if provided.
        /// </summary>
        [JsonProperty("full_accuracy_value")]
        public string FullAccuracyValue { get; set; }

        /// <summary>
        /// Source timestamp in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// System receive timestamp in milliseconds.
        /// </summary>
        [JsonProperty("received_at")]
        public long? ReceivedAt { get; set; }

        /// <summary>
        /// True when a market is closed and the last known value is repeated.
        /// </summary>
        [JsonProperty("is_carried_forward")]
        public bool? IsCarriedForward { get; set; }
    }
}
