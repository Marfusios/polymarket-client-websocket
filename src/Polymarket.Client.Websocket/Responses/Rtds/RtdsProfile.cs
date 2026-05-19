using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Rtds
{
    /// <summary>
    /// RTDS comment profile payload.
    /// </summary>
    public class RtdsProfile
    {
        /// <summary>
        /// Base address.
        /// </summary>
        [JsonProperty("baseAddress")]
        public string BaseAddress { get; set; }

        /// <summary>
        /// Whether username is public.
        /// </summary>
        [JsonProperty("displayUsernamePublic")]
        public bool? DisplayUsernamePublic { get; set; }

        /// <summary>
        /// Display name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Proxy wallet.
        /// </summary>
        [JsonProperty("proxyWallet")]
        public string ProxyWallet { get; set; }

        /// <summary>
        /// Pseudonym.
        /// </summary>
        [JsonProperty("pseudonym")]
        public string Pseudonym { get; set; }
    }
}
