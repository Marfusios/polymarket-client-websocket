using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Orderbook price level.
    /// </summary>
    public class OrderBookLevel
    {
        /// <summary>
        /// Price as sent by Polymarket.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Size as sent by Polymarket.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }
    }
}
