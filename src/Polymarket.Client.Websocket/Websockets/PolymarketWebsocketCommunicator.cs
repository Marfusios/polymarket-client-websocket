using System;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using Polymarket.Client.Websocket.Communicator;
using Websocket.Client;

namespace Polymarket.Client.Websocket.Websockets
{
    /// <inheritdoc cref="WebsocketClient" />
    public class PolymarketWebsocketCommunicator : WebsocketClient, IPolymarketCommunicator
    {
        /// <inheritdoc />
        public PolymarketWebsocketCommunicator(Uri url, Func<ClientWebSocket>? clientFactory = null)
            : base(url, clientFactory)
        {
        }

        /// <inheritdoc />
        public PolymarketWebsocketCommunicator(Uri url, ILogger<PolymarketWebsocketCommunicator> logger, Func<ClientWebSocket>? clientFactory = null)
            : base(url, logger, clientFactory)
        {
        }
    }
}
