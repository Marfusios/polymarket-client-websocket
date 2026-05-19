using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Polymarket CLOB API credentials for the user websocket channel.
    /// </summary>
    public class UserAuth
    {
        /// <summary>
        /// Create user-channel authentication payload.
        /// </summary>
        public UserAuth(string apiKey, string secret, string passphrase)
        {
            PolyValidations.ValidateNotEmpty(apiKey, nameof(apiKey));
            PolyValidations.ValidateNotEmpty(secret, nameof(secret));
            PolyValidations.ValidateNotEmpty(passphrase, nameof(passphrase));

            ApiKey = apiKey;
            Secret = secret;
            Passphrase = passphrase;
        }

        /// <summary>
        /// API key.
        /// </summary>
        [JsonProperty("apiKey")]
        public string ApiKey { get; }

        /// <summary>
        /// API secret.
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; }

        /// <summary>
        /// API passphrase.
        /// </summary>
        [JsonProperty("passphrase")]
        public string Passphrase { get; }
    }
}
