using System.Linq;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// RTDS subscription entry.
    /// </summary>
    public class RtdsSubscription
    {
        /// <summary>
        /// Create RTDS subscription.
        /// </summary>
        public RtdsSubscription(string topic, string type, string filters = null, RtdsGammaAuth gammaAuth = null)
        {
            PolyValidations.ValidateNotEmpty(topic, nameof(topic));
            PolyValidations.ValidateNotEmpty(type, nameof(type));

            Topic = topic;
            Type = type;
            Filters = filters;
            GammaAuth = gammaAuth;
        }

        /// <summary>
        /// Subscribe to Binance-sourced crypto prices.
        /// </summary>
        public static RtdsSubscription CryptoPrices(params string[] symbols)
        {
            var filters = symbols == null || symbols.Length == 0
                ? null
                : JsonConvert.SerializeObject(symbols.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.ToLowerInvariant()).ToArray());

            return new RtdsSubscription("crypto_prices", "update", filters);
        }

        /// <summary>
        /// Subscribe to Chainlink-sourced crypto prices.
        /// </summary>
        public static RtdsSubscription ChainlinkCryptoPrices(string symbol = null)
        {
            var filters = string.IsNullOrWhiteSpace(symbol) ? string.Empty : "{\"symbol\":\"" + symbol.ToLowerInvariant() + "\"}";
            return new RtdsSubscription("crypto_prices_chainlink", "*", filters);
        }

        /// <summary>
        /// Subscribe to equity price updates for a symbol.
        /// </summary>
        public static RtdsSubscription EquityPrices(string symbol, bool includeSnapshot = true)
        {
            PolyValidations.ValidateNotEmpty(symbol, nameof(symbol));

            var filters = "{\"symbol\":\"" + symbol.ToUpperInvariant() + "\"}";
            return new RtdsSubscription("equity_prices", includeSnapshot ? "*" : "update", filters);
        }

        /// <summary>
        /// Subscribe to comment events.
        /// </summary>
        public static RtdsSubscription Comments(string type = "comment_created", RtdsGammaAuth gammaAuth = null)
        {
            return new RtdsSubscription("comments", type, null, gammaAuth);
        }

        /// <summary>
        /// Subscription topic.
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; }

        /// <summary>
        /// Message type or wildcard.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; }

        /// <summary>
        /// Optional filter string.
        /// </summary>
        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public string Filters { get; }

        /// <summary>
        /// Optional Gamma auth.
        /// </summary>
        [JsonProperty("gamma_auth", NullValueHandling = NullValueHandling.Ignore)]
        public RtdsGammaAuth GammaAuth { get; }
    }
}
