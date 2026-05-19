namespace Polymarket.Client.Websocket.Responses
{
    /// <summary>
    /// Response to a websocket heartbeat.
    /// </summary>
    public class PongResponse
    {
        /// <summary>
        /// Create pong response.
        /// </summary>
        public PongResponse(string message = "PONG")
        {
            Message = message;
        }

        /// <summary>
        /// Raw pong message.
        /// </summary>
        public string Message { get; }
    }
}
