namespace Polymarket.Client.Websocket.Responses
{
    /// <summary>
    /// Server heartbeat received by channels where the client must respond.
    /// </summary>
    public class PingResponse
    {
        /// <summary>
        /// Create ping response.
        /// </summary>
        public PingResponse(string message = "ping")
        {
            Message = message;
        }

        /// <summary>
        /// Raw ping message.
        /// </summary>
        public string Message { get; }
    }
}
