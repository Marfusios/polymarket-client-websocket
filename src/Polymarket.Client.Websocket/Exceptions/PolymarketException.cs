using System;

namespace Polymarket.Client.Websocket.Exceptions
{
    /// <summary>
    /// Base exception for Polymarket client errors.
    /// </summary>
    public class PolymarketException : Exception
    {
        /// <inheritdoc />
        public PolymarketException()
        {
        }

        /// <inheritdoc />
        public PolymarketException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public PolymarketException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
