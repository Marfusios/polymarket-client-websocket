using System;
using System.IO;
using System.Threading.Tasks;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Files;
using Polymarket.Client.Websocket.Responses.Market;
using Xunit;

namespace Polymarket.Client.Websocket.Tests.Integration
{
    public class PolymarketFileCommunicatorTests
    {
        [Fact]
        [Trait("Cat", "Base")]
        public async Task Start_WhenRawFileProvided_ReplaysMessages()
        {
            var fileName = Path.Combine(AppContext.BaseDirectory, "data", "polymarket_raw_sample.txt");
            using var communicator = new PolymarketFileCommunicator
            {
                FileNames = new[] { fileName },
                Delimiter = ";;"
            };
            using var client = new PolymarketWebsocketClient(communicator);
            OrderBookSnapshotResponse book = null;
            PriceChangeResponse priceChange = null;

            client.Streams.OrderBookStream.Subscribe(x => book = x);
            client.Streams.PriceChangeStream.Subscribe(x => priceChange = x);

            await communicator.Start();

            Assert.NotNull(book);
            Assert.NotNull(priceChange);
            Assert.Equal("condition-1", book.Market);
            Assert.Equal("condition-1", priceChange.Market);
        }
    }
}
