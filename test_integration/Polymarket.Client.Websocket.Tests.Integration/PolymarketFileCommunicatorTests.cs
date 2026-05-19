using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Enums;
using Polymarket.Client.Websocket.Files;
using Polymarket.Client.Websocket.Responses.Market;
using Polymarket.Client.Websocket.Responses.Rtds;
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
            Assert.Equal(0.48m, book.Bids[0].Price);
            Assert.Equal(0.5m, priceChange.PriceChanges[0].Price);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public async Task Start_WhenCollectedPublicReplayProvided_RoutesTypedStreams()
        {
            var fileName = Path.Combine(AppContext.BaseDirectory, "data", "polymarket_public_replay.txt");
            using var communicator = new PolymarketFileCommunicator
            {
                FileNames = new[] { fileName },
                Delimiter = ";;"
            };
            using var client = new PolymarketWebsocketClient(communicator);
            var rawMessages = new List<string>();
            var orderBooks = new List<OrderBookSnapshotResponse>();
            var priceChanges = new List<PriceChangeResponse>();
            var bestBidAsks = new List<BestBidAskResponse>();
            var rtdsMessages = new List<RtdsMessage>();
            var cryptoPrices = new List<RtdsPricePayload>();
            var equitySnapshots = new List<RtdsEquitySnapshotPayload>();
            var equityPrices = new List<RtdsPricePayload>();
            var comments = new List<RtdsCommentPayload>();

            client.Streams.RawMessageStream.Subscribe(rawMessages.Add);
            client.Streams.OrderBookStream.Subscribe(orderBooks.Add);
            client.Streams.PriceChangeStream.Subscribe(priceChanges.Add);
            client.Streams.BestBidAskStream.Subscribe(bestBidAsks.Add);
            client.Streams.RtdsMessageStream.Subscribe(rtdsMessages.Add);
            client.Streams.RtdsCryptoPriceStream.Subscribe(cryptoPrices.Add);
            client.Streams.RtdsEquitySnapshotStream.Subscribe(equitySnapshots.Add);
            client.Streams.RtdsEquityPriceStream.Subscribe(equityPrices.Add);
            client.Streams.RtdsCommentStream.Subscribe(comments.Add);

            await communicator.Start();

            Assert.Equal(240, rawMessages.Count);
            Assert.Equal(24, orderBooks.Count);
            Assert.Equal(109, priceChanges.Count);
            Assert.Equal(10, bestBidAsks.Count);
            Assert.Equal(120, rtdsMessages.Count);
            Assert.Equal(54, cryptoPrices.Count);
            Assert.Equal(2, equitySnapshots.Count);
            Assert.Equal(55, equityPrices.Count);
            Assert.Equal(9, comments.Count);
            Assert.Equal(PolymarketEventType.Book, orderBooks[0].EventType);
            Assert.True(orderBooks[0].Bids[0].Price > 0);
            Assert.Equal(PolymarketEventType.PriceChange, priceChanges[0].EventType);
            Assert.NotEqual(PolymarketOrderSide.Unknown, priceChanges[0].PriceChanges[0].Side);
            Assert.True(priceChanges[0].PriceChanges[0].Price > 0);
            Assert.Equal(PolymarketEventType.BestBidAsk, bestBidAsks[0].EventType);
            Assert.True(bestBidAsks[0].BestBid > 0);
            Assert.Contains(rtdsMessages, x => x.Topic == RtdsTopic.CryptoPricesChainlink);
            Assert.Contains(rtdsMessages, x => x.Topic == RtdsTopic.EquityPrices && x.Type == RtdsMessageType.Subscribe);
            Assert.Contains(cryptoPrices, x => x.Value.HasValue && x.Value.Value > 0);
            Assert.All(equitySnapshots, x => Assert.NotEmpty(x.Data));
            Assert.Contains(equityPrices, x => x.Symbol.Equals("aapl", StringComparison.OrdinalIgnoreCase) && x.Value.HasValue);
            Assert.All(comments, x => Assert.Equal("sample comment body", x.Body));
            Assert.All(comments.Where(x => x.Profile != null), x => Assert.Equal("sample-user", x.Profile.Name));
        }
    }
}
