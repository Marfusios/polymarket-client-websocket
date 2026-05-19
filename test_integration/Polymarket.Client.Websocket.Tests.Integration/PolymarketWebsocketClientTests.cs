using System;
using System.Threading;
using System.Threading.Tasks;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Requests;
using Polymarket.Client.Websocket.Responses.Market;
using Polymarket.Client.Websocket.Websockets;
using Xunit;

namespace Polymarket.Client.Websocket.Tests.Integration
{
    public class PolymarketWebsocketClientTests
    {
        [Fact(Skip = "Live Polymarket websocket smoke test. Enable manually with a current active asset ID.")]
        [Trait("Cat", "BaseExtended")]
        public async Task MarketChannel_WhenSubscribed_ReceivesOrderBookSnapshot()
        {
            var assetId = Environment.GetEnvironmentVariable("POLYMARKET_ASSET_ID");
            Assert.False(string.IsNullOrWhiteSpace(assetId), "Set POLYMARKET_ASSET_ID to an active CLOB token ID.");

            using var communicator = new PolymarketWebsocketCommunicator(PolymarketValues.MarketWebsocketApiUrl);
            using var client = new PolymarketWebsocketClient(communicator);
            OrderBookSnapshotResponse received = null;
            var receivedEvent = new ManualResetEvent(false);

            client.Streams.OrderBookStream.Subscribe(book =>
            {
                received = book;
                receivedEvent.Set();
            });

            await communicator.Start();
            client.Send(new MarketSubscriptionRequest(new[] { assetId }));
            client.StartHeartbeat(TimeSpan.FromSeconds(10));

            receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

            Assert.NotNull(received);
        }
    }
}
