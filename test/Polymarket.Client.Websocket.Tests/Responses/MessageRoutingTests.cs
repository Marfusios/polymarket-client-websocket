using System;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Enums;
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
            Assert.Equal(PolymarketEventType.Book, received.EventType);
            Assert.Equal("asset-1", received.AssetId);
            Assert.Single(received.Bids);
            Assert.Single(received.Asks);
            Assert.Equal(0.48m, received.Bids[0].Price);
            Assert.Equal(30m, received.Bids[0].Size);
            Assert.Equal(1757908892351L, received.Timestamp);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenUserTradeReceived_PublishesUserTradeStream()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            TradeEventResponse received = null;

            client.Streams.UserTradeStream.Subscribe(x => received = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"event_type\":\"trade\",\"type\":\"TRADE\",\"id\":\"trade-1\",\"market\":\"condition-1\",\"asset_id\":\"asset-1\",\"side\":\"BUY\",\"size\":\"10\",\"price\":\"0.57\",\"fee_rate_bps\":\"0\",\"status\":\"MATCHED\",\"matchtime\":\"1672290701\",\"last_update\":\"1672290702\",\"maker_orders\":[{\"order_id\":\"order-1\",\"matched_amount\":\"10\",\"price\":\"0.57\",\"fee_rate_bps\":\"\",\"asset_id\":\"asset-1\",\"side\":\"SELL\"}],\"trader_side\":\"TAKER\",\"timestamp\":\"1672290703\"}"));

            Assert.NotNull(received);
            Assert.Equal("trade-1", received.Id);
            Assert.Equal(PolymarketTradeEventType.Trade, received.Type);
            Assert.Equal(PolymarketOrderSide.Buy, received.Side);
            Assert.Equal(10m, received.Size);
            Assert.Equal(0.57m, received.Price);
            Assert.Equal(PolymarketTradeStatus.Matched, received.Status);
            Assert.Equal(1672290701L, received.MatchTime);
            Assert.Equal(1672290702L, received.LastUpdate);
            Assert.Equal(PolymarketTradeRole.Taker, received.TraderSide);
            Assert.Equal(1672290703L, received.Timestamp);
            Assert.Single(received.MakerOrders);
            Assert.Equal(10m, received.MakerOrders[0].MatchedAmount);
            Assert.Equal(0m, received.MakerOrders[0].FeeRateBps);
            Assert.Equal(PolymarketOrderSide.Sell, received.MakerOrders[0].Side);
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
            Assert.Equal(RtdsTopic.CryptoPrices, raw.Topic);
            Assert.Equal(RtdsMessageType.Update, raw.Type);
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

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenBestBidAskUsesNumericTokens_PublishesTypedDecimalsAndEnums()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            BestBidAskResponse received = null;

            client.Streams.BestBidAskStream.Subscribe(x => received = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"event_type\":\"best_bid_ask\",\"asset_id\":\"asset-1\",\"market\":\"condition-1\",\"best_bid\":0.48,\"best_ask\":0.50,\"spread\":\"0.02\",\"timestamp\":\"1757908892351\"}"));

            Assert.NotNull(received);
            Assert.Equal(PolymarketEventType.BestBidAsk, received.EventType);
            Assert.Equal(0.48m, received.BestBid);
            Assert.Equal(0.50m, received.BestAsk);
            Assert.Equal(0.02m, received.Spread);
            Assert.Equal(1757908892351L, received.Timestamp);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void HandleMessage_WhenUserOrderReceived_PublishesTypedOrderFields()
        {
            using var communicator = new PolymarketFileCommunicator();
            using var client = new PolymarketWebsocketClient(communicator);
            OrderEventResponse received = null;

            client.Streams.UserOrderStream.Subscribe(x => received = x);

            communicator.StreamFakeMessage(ResponseMessage.TextMessage("{\"event_type\":\"order\",\"id\":\"order-1\",\"owner\":\"owner-1\",\"market\":\"condition-1\",\"asset_id\":\"asset-1\",\"side\":\"SELL\",\"order_owner\":\"owner-1\",\"original_size\":\"10\",\"size_matched\":\"0\",\"price\":\"0.57\",\"associate_trades\":null,\"outcome\":\"YES\",\"type\":\"PLACEMENT\",\"created_at\":\"1672290687\",\"expiration\":\"1234567\",\"order_type\":\"GTD\",\"status\":\"LIVE\",\"maker_address\":\"0x1234\",\"timestamp\":\"1672290688\"}"));

            Assert.NotNull(received);
            Assert.Equal(PolymarketEventType.Order, received.EventType);
            Assert.Equal(PolymarketOrderSide.Sell, received.Side);
            Assert.Equal(10m, received.OriginalSize);
            Assert.Equal(0m, received.SizeMatched);
            Assert.Equal(0.57m, received.Price);
            Assert.Equal(PolymarketOrderUpdateType.Placement, received.Type);
            Assert.Equal(1672290687L, received.CreatedAt);
            Assert.Equal(1234567L, received.Expiration);
            Assert.Equal(PolymarketTimeInForce.GoodTillDate, received.OrderType);
            Assert.Equal(PolymarketOrderStatus.Live, received.Status);
            Assert.Equal(1672290688L, received.Timestamp);
        }
    }
}
