using Newtonsoft.Json;
using Polymarket.Client.Websocket.Validations;

namespace Polymarket.Client.Websocket.Requests
{
    /// <summary>
    /// Optional Gamma authentication for RTDS user-specific streams.
    /// </summary>
    public class RtdsGammaAuth
    {
        /// <summary>
        /// Create Gamma auth payload.
        /// </summary>
        public RtdsGammaAuth(string address)
        {
            PolyValidations.ValidateNotEmpty(address, nameof(address));
            Address = address;
        }

        /// <summary>
        /// Wallet address.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; }
    }
}
