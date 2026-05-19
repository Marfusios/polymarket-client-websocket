using System.Collections.Generic;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Initial subscription request for the public market channel.
    /// </summary>
    public class MarketSubscriptionRequest
    {
        /// <summary>
        /// Create market subscription request.
        /// </summary>
        public MarketSubscriptionRequest(IEnumerable<string> assetIds, bool customFeatureEnabled = false)
        {
            AssetIds = PolyValidations.ValidateArray(assetIds, nameof(assetIds));
            CustomFeatureEnabled = customFeatureEnabled ? (bool?)true : null;
        }

        /// <summary>
        /// Token IDs to subscribe to.
        /// </summary>
        [JsonProperty("assets_ids")]
        public string[] AssetIds { get; }

        /// <summary>
        /// Channel identifier.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; } = "market";

        /// <summary>
        /// Enables best_bid_ask, new_market, and market_resolved messages.
        /// </summary>
        [JsonProperty("custom_feature_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CustomFeatureEnabled { get; }
    }
}
