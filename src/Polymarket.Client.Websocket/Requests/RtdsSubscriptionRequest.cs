using System.Collections.Generic;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// RTDS subscribe or unsubscribe request.
    /// </summary>
    public class RtdsSubscriptionRequest
    {
        /// <summary>
        /// Create RTDS subscription request.
        /// </summary>
        public RtdsSubscriptionRequest(string action, IEnumerable<RtdsSubscription> subscriptions)
        {
            PolyValidations.ValidateOperation(action);
            PolyValidations.ValidateInput(subscriptions, nameof(subscriptions));

            Action = action;
            Subscriptions = new List<RtdsSubscription>(subscriptions).ToArray();
            if (Subscriptions.Length == 0)
            {
                throw new Exceptions.PolymarketBadInputException("At least one RTDS subscription is required");
            }
        }

        /// <summary>
        /// Create subscribe request.
        /// </summary>
        public static RtdsSubscriptionRequest Subscribe(params RtdsSubscription[] subscriptions)
        {
            return new RtdsSubscriptionRequest("subscribe", subscriptions);
        }

        /// <summary>
        /// Create unsubscribe request.
        /// </summary>
        public static RtdsSubscriptionRequest Unsubscribe(params RtdsSubscription[] subscriptions)
        {
            return new RtdsSubscriptionRequest("unsubscribe", subscriptions);
        }

        /// <summary>
        /// Action, either subscribe or unsubscribe.
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; }

        /// <summary>
        /// Subscription list.
        /// </summary>
        [JsonProperty("subscriptions")]
        public RtdsSubscription[] Subscriptions { get; }
    }
}
