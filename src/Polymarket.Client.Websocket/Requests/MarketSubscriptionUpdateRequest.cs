using System.Collections.Generic;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe or unsubscribe market-channel asset IDs without reconnecting.
    /// </summary>
    public class MarketSubscriptionUpdateRequest
    {
        /// <summary>
        /// Create market subscription update.
        /// </summary>
        public MarketSubscriptionUpdateRequest(string operation, IEnumerable<string> assetIds, bool customFeatureEnabled = false)
        {
            PolyValidations.ValidateOperation(operation);

            Operation = operation;
            AssetIds = PolyValidations.ValidateArray(assetIds, nameof(assetIds));
            CustomFeatureEnabled = customFeatureEnabled ? (bool?)true : null;
        }

        /// <summary>
        /// Create subscribe request.
        /// </summary>
        public static MarketSubscriptionUpdateRequest Subscribe(IEnumerable<string> assetIds, bool customFeatureEnabled = false)
        {
            return new MarketSubscriptionUpdateRequest("subscribe", assetIds, customFeatureEnabled);
        }

        /// <summary>
        /// Create unsubscribe request.
        /// </summary>
        public static MarketSubscriptionUpdateRequest Unsubscribe(IEnumerable<string> assetIds)
        {
            return new MarketSubscriptionUpdateRequest("unsubscribe", assetIds);
        }

        /// <summary>
        /// Operation, either subscribe or unsubscribe.
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; }

        /// <summary>
        /// Token IDs to modify.
        /// </summary>
        [JsonProperty("assets_ids")]
        public string[] AssetIds { get; }

        /// <summary>
        /// Enables custom market-channel messages.
        /// </summary>
        [JsonProperty("custom_feature_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CustomFeatureEnabled { get; }
    }
}
