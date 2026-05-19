using System.Collections.Generic;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe or unsubscribe user-channel condition IDs without reconnecting.
    /// </summary>
    public class UserSubscriptionUpdateRequest
    {
        /// <summary>
        /// Create user subscription update.
        /// </summary>
        public UserSubscriptionUpdateRequest(string operation, IEnumerable<string> markets)
        {
            PolyValidations.ValidateOperation(operation);

            Operation = operation;
            Markets = PolyValidations.ValidateArray(markets, nameof(markets));
        }

        /// <summary>
        /// Create subscribe request.
        /// </summary>
        public static UserSubscriptionUpdateRequest Subscribe(IEnumerable<string> markets)
        {
            return new UserSubscriptionUpdateRequest("subscribe", markets);
        }

        /// <summary>
        /// Create unsubscribe request.
        /// </summary>
        public static UserSubscriptionUpdateRequest Unsubscribe(IEnumerable<string> markets)
        {
            return new UserSubscriptionUpdateRequest("unsubscribe", markets);
        }

        /// <summary>
        /// Operation, either subscribe or unsubscribe.
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; }

        /// <summary>
        /// Condition IDs to modify.
        /// </summary>
        [JsonProperty("markets")]
        public string[] Markets { get; }
    }
}
