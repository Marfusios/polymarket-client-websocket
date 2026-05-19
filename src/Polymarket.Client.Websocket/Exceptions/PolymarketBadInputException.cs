namespace Polymarket.Client.Websocket.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid value is passed to the client.
    /// </summary>
    public class PolymarketBadInputException : PolymarketException
    {
        /// <inheritdoc />
        public PolymarketBadInputException(string message)
            : base(message)
        {
        }
    }
}
