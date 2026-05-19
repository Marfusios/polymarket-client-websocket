using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Market
{
    /// <summary>
    /// Orderbook price level.
    /// </summary>
    public class OrderBookLevel
    {
        /// <summary>
        /// Price.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }
    }
}
