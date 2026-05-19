using Polymarket.Client.Websocket.Json;
using Polymarket.Client.Websocket.Requests;
using Xunit;

namespace Polymarket.Client.Websocket.Tests.Requests
{
    public class RequestSerializationTests
    {
        [Fact]
        [Trait("Cat", "Base")]
        public void MarketSubscriptionRequest_WhenCustomFeaturesEnabled_SerializesExpectedPayload()
        {
            var request = new MarketSubscriptionRequest(new[] { "asset-1", "asset-2" }, true);

            var json = PolymarketJsonSerializer.Serialize(request);

            Assert.Equal("{\"assets_ids\":[\"asset-1\",\"asset-2\"],\"type\":\"market\",\"custom_feature_enabled\":true}", json);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void UserSubscriptionRequest_WhenMarketsProvided_SerializesExpectedPayload()
        {
            var auth = new UserAuth("api-key", "secret", "passphrase");
            var request = new UserSubscriptionRequest(auth, new[] { "condition-1" });

            var json = PolymarketJsonSerializer.Serialize(request);

            Assert.Equal("{\"auth\":{\"apiKey\":\"api-key\",\"secret\":\"secret\",\"passphrase\":\"passphrase\"},\"markets\":[\"condition-1\"],\"type\":\"user\"}", json);
        }

        [Fact]
        [Trait("Cat", "Base")]
        public void RtdsSubscriptionRequest_WhenCryptoSymbolsProvided_SerializesExpectedPayload()
        {
            var request = RtdsSubscriptionRequest.Subscribe(RtdsSubscription.CryptoPrices("BTCUSDT", "ETHUSDT"));

            var json = PolymarketJsonSerializer.Serialize(request);

            Assert.Equal("{\"action\":\"subscribe\",\"subscriptions\":[{\"topic\":\"crypto_prices\",\"type\":\"update\",\"filters\":\"[\\\"btcusdt\\\",\\\"ethusdt\\\"]\"}]}", json);
        }
    }
}
