using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Rtds
{
    /// <summary>
    /// RTDS equity historical snapshot payload.
    /// </summary>
    public class RtdsEquitySnapshotPayload
    {
        /// <summary>
        /// Symbol.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Snapshot data.
        /// </summary>
        [JsonProperty("data")]
        public RtdsPricePayload[] Data { get; set; }
    }
}
