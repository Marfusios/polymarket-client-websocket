using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Polymarket.Client.Websocket.Json;
using Polymarket.Client.Websocket.Responses;
using Polymarket.Client.Websocket.Responses.Market;
using Polymarket.Client.Websocket.Responses.Rtds;
using Polymarket.Client.Websocket.Responses.Sports;
using Polymarket.Client.Websocket.Responses.User;

namespace Polymarket.Client.Websocket.Client
{
    internal class PolymarketMessageHandler
    {
        private readonly PolymarketClientStreams _streams;
        private readonly ILogger _logger;

        public PolymarketMessageHandler(PolymarketClientStreams streams, ILogger logger)
        {
            _streams = streams;
            _logger = logger;
        }

        public void HandleMessage(string message)
        {
            if (string.Equals(message, "PONG", StringComparison.OrdinalIgnoreCase))
            {
                _streams.PongSubject.OnNext(new PongResponse(message));
                return;
            }

            var token = JToken.Parse(message);
            HandleToken(token);
        }

        private void HandleToken(JToken token)
        {
            if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.Children())
                {
                    HandleToken(item);
                }

                return;
            }

            var obj = token as JObject;
            if (obj == null)
            {
                _logger.LogDebug("Unhandled non-object message: {message}", token.ToString());
                return;
            }

            if (!obj.HasValues)
            {
                _streams.PongSubject.OnNext(new PongResponse("{}"));
                return;
            }

            var eventType = obj["event_type"]?.Value<string>();
            if (!string.IsNullOrEmpty(eventType))
            {
                HandleEventMessage(eventType, obj);
                return;
            }

            var topic = obj["topic"]?.Value<string>();
            if (!string.IsNullOrEmpty(topic))
            {
                HandleRtdsMessage(topic, obj);
                return;
            }

            if (IsSportsResult(obj))
            {
                Publish(obj, _streams.SportsResultSubject);
                return;
            }

            _logger.LogDebug("Unhandled object message: {message}", obj.ToString());
        }

        private void HandleEventMessage(string eventType, JObject obj)
        {
            switch (eventType)
            {
                case "book":
                    Publish(obj, _streams.OrderBookSubject);
                    break;
                case "price_change":
                    Publish(obj, _streams.PriceChangeSubject);
                    break;
                case "last_trade_price":
                    Publish(obj, _streams.LastTradePriceSubject);
                    break;
                case "tick_size_change":
                    Publish(obj, _streams.TickSizeChangeSubject);
                    break;
                case "best_bid_ask":
                    Publish(obj, _streams.BestBidAskSubject);
                    break;
                case "new_market":
                    Publish(obj, _streams.NewMarketSubject);
                    break;
                case "market_resolved":
                    Publish(obj, _streams.MarketResolvedSubject);
                    break;
                case "order":
                    Publish(obj, _streams.UserOrderSubject);
                    break;
                case "trade":
                    Publish(obj, _streams.UserTradeSubject);
                    break;
                default:
                    _logger.LogDebug("Unhandled event_type: {eventType}", eventType);
                    break;
            }
        }

        private void HandleRtdsMessage(string topic, JObject obj)
        {
            var message = obj.ToObject<RtdsMessage>(PolymarketJsonSerializer.Serializer);
            if (message == null)
            {
                return;
            }

            _streams.RtdsMessageSubject.OnNext(message);

            if (message.Payload == null)
            {
                return;
            }

            switch (topic)
            {
                case "crypto_prices":
                case "crypto_prices_chainlink":
                    Publish(message.Payload, _streams.RtdsCryptoPriceSubject);
                    break;
                case "equity_prices":
                    if (string.Equals(message.Type, "subscribe", StringComparison.OrdinalIgnoreCase))
                    {
                        Publish(message.Payload, _streams.RtdsEquitySnapshotSubject);
                    }
                    else
                    {
                        Publish(message.Payload, _streams.RtdsEquityPriceSubject);
                    }

                    break;
                case "comments":
                    Publish(message.Payload, _streams.RtdsCommentSubject);
                    break;
                default:
                    _logger.LogDebug("Unhandled RTDS topic: {topic}", topic);
                    break;
            }
        }

        private static bool IsSportsResult(JObject obj)
        {
            return obj["slug"] != null &&
                   (obj["score"] != null || obj["period"] != null || obj["last_update"] != null);
        }

        private static void Publish<T>(JToken token, IObserver<T> subject)
        {
            var response = token.ToObject<T>(PolymarketJsonSerializer.Serializer);
            if (response != null)
            {
                subject.OnNext(response);
            }
        }
    }
}
