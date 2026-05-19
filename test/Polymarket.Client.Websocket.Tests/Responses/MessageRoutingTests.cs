using System;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Files;
using Polymarket.Client.Websocket.Responses.Market;
using Polymarket.Client.Websocket.Responses.Rtds;
using Polymarket.Client.Websocket.Responses.User;
using Websocket.Client;
using Xunit;

namespace Polymarket.Client.Websocket.Tests.Responses
{
    public class MessageRoutingTests
    {
        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenBookEventReceived_PublishesOrderBookStream()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            OrderBookSnapshotResponse received = null;

            client.Streams.OrderBookStream.Subscribe(x => received = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"event_type\":\"book\",\"asset_id\":\"asset-1\",\"market\":\"condition-1\",\"bids\":[{\"price\":\"0.48\",\"size\":\"30\"}],\"asks\":[{\"price\":\"0.52\",\"size\":\"25\"}],\"timestamp\":\"1757908892351\",\"hash\":\"hash-1\"}"));

            Assert.NotNull(received);
            Assert.Equal("asset-1", received.AssetId);
            Assert.Single(received.Bids);
            Assert.Single(received.Asks);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenUserTradeReceived_PublishesUserTradeStream()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            TradeEventResponse received = null;

            client.Streams.UserTradeStream.Subscribe(x => received = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"event_type\":\"trade\",\"type\":\"TRADE\",\"id\":\"trade-1\",\"market\":\"condition-1\",\"asset_id\":\"asset-1\",\"side\":\"BUY\",\"size\":\"10\",\"price\":\"0.57\",\"status\":\"MATCHED\",\"maker_orders\":[{\"order_id\":\"order-1\",\"matched_amount\":\"10\",\"price\":\"0.57\",\"asset_id\":\"asset-1\",\"side\":\"SELL\"}],\"timestamp\":\"1672290701\"}"));

            Assert.NotNull(received);
            Assert.Equal("trade-1", received.Id);
            Assert.Single(received.MakerOrders);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenRtdsCryptoPriceReceived_PublishesRtdsStreams()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            RtdsMessage raw = null;
            RtdsPricePayload price = null;

            client.Streams.RtdsMessageStream.Subscribe(x => raw = x);
            client.Streams.RtdsCryptoPriceStream.Subscribe(x => price = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"topic\":\"crypto_prices\",\"type\":\"update\",\"timestamp\":1753314064237,\"payload\":{\"symbol\":\"btcusdt\",\"timestamp\":1753314064213,\"value\":67234.50}}"));

            Assert.NotNull(raw);
            Assert.NotNull(price);
            Assert.Equal("crypto_prices", raw.Topic);
            Assert.Equal("btcusdt", price.Symbol);
            Assert.Equal(67234.50m, price.Value);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenArrayReceived_PublishesEachEvent()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            var count = 0;

            client.Streams.LastTradePriceStream.Subscribe(_ => count++);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("[{\"event_type\":\"last_trade_price\",\"asset_id\":\"asset-1\",\"market\":\"condition-1\",\"price\":\"0.456\",\"size\":\"219.217767\",\"side\":\"BUY\",\"timestamp\":\"1750428146322\"},{\"event_type\":\"last_trade_price\",\"asset_id\":\"asset-2\",\"market\":\"condition-1\",\"price\":\"0.544\",\"size\":\"10\",\"side\":\"SELL\",\"timestamp\":\"1750428146323\"}]"));

            Assert.Equal(2, count);
        }
    }
}
