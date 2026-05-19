using System.Collections.Generic;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Initial authenticated subscription request for the user channel.
    /// </summary>
    public class UserSubscriptionRequest
    {
        /// <summary>
        /// Create user subscription request.
        /// </summary>
        public UserSubscriptionRequest(UserAuth auth, IEnumerable<string> markets = null)
        {
            PolyValidations.ValidateInput(auth, nameof(auth));

            Auth = auth;
            Markets = markets == null ? null : PolyValidations.ValidateArray(markets, nameof(markets));
        }

        /// <summary>
        /// API credentials.
        /// </summary>
        [JsonProperty("auth")]
        public UserAuth Auth { get; }

        /// <summary>
        /// Condition IDs to receive events for.
        /// </summary>
        [JsonProperty("markets", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Markets { get; }

        /// <summary>
        /// Channel identifier.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; } = "user";
    }
}
