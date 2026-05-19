using System;

namespace Polymarket.Client.Websocket
{
    /// <summary>
    /// Polymarket websocket endpoints.
    /// </summary>
    public static class PolymarketValues
    {
        /// <summary>
        /// Public CLOB market channel for order books, price changes, trades, and market lifecycle events.
        /// </summary>
        public static readonly Uri MarketWebsocketApiUrl = new Uri("wss://ws-subscriptions-clob.polymarket.com/ws/market");

        /// <summary>
        /// Authenticated CLOB user channel for order and trade updates.
        /// </summary>
        public static readonly Uri UserWebsocketApiUrl = new Uri("wss://ws-subscriptions-clob.polymarket.com/ws/user");

        /// <summary>
        /// Public sports channel for live sports result updates.
        /// </summary>
        public static readonly Uri SportsWebsocketApiUrl = new Uri("wss://sports-api.polymarket.com/ws");

        /// <summary>
        /// Real-Time Data Socket for crypto prices, equity prices, and comments.
        /// </summary>
        public static readonly Uri RtdsWebsocketApiUrl = new Uri("wss://ws-live-data.polymarket.com");
    }
}
