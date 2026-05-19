![Logo](https://raw.githubusercontent.com/Marfusios/polymarket-client-websocket/master/polymarket-logo.png)
# Polymarket websocket API client

[![NuGet version](https://img.shields.io/nuget/v/Polymarket.Client.Websocket?style=flat-square)](https://www.nuget.org/packages/Polymarket.Client.Websocket)
[![Nuget downloads](https://img.shields.io/nuget/dt/Polymarket.Client.Websocket?style=flat-square)](https://www.nuget.org/packages/Polymarket.Client.Websocket)
[![CI build](https://img.shields.io/github/check-runs/marfusios/polymarket-client-websocket/master?style=flat-square&label=build)](https://github.com/Marfusios/polymarket-client-websocket/actions/workflows/dotnet-core.yml)

This is a C# implementation of the Polymarket websocket APIs:

- [WebSocket overview](https://docs.polymarket.com/market-data/websocket/overview)
- [Market channel](https://docs.polymarket.com/api-reference/wss/market)
- [User channel](https://docs.polymarket.com/api-reference/wss/user)
- [Sports channel](https://docs.polymarket.com/api-reference/wss/sports)
- [RTDS](https://docs.polymarket.com/market-data/websocket/rtds)

[Releases and breaking changes](https://github.com/Marfusios/polymarket-client-websocket/releases)

### License

Apache License 2.0

### Features

- installation via NuGet ([Polymarket.Client.Websocket](https://www.nuget.org/packages/Polymarket.Client.Websocket))
- public market API, authenticated user API, sports API, and RTDS
- targets `netstandard2.0`, `netstandard2.1`, `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`
- built on [Websocket.Client 5.5.0](https://www.nuget.org/packages/Websocket.Client/5.5.0) for websocket transport, reconnects, and lower-allocation message handling
- typed response models: Polymarket stringified prices/sizes are parsed to `decimal`, timestamps to integer values, and known sides/statuses/topics to enums
- reactive extensions ([Rx.NET](https://github.com/Reactive-Extensions/Rx.NET))
- file communicator for replay/backtesting and data collection pipelines, with tests covering collected public market and RTDS websocket frames

### Usage

Market channel:

```csharp
var assetIds = new[] { "217...token-id" };
var url = PolymarketValues.MarketWebsocketApiUrl;

using var communicator = new PolymarketWebsocketCommunicator(url);
using var client = new PolymarketWebsocketClient(communicator);

client.Streams.OrderBookStream.Subscribe(book =>
{
    Console.WriteLine($"Book {book.AssetId}: bids={book.Bids.Length}, asks={book.Asks.Length}");
});

client.Streams.PriceChangeStream.Subscribe(change =>
{
    Console.WriteLine($"Price changes for {change.Market}: {change.PriceChanges.Length}");
});

await communicator.Start();

client.Send(new MarketSubscriptionRequest(assetIds, customFeatureEnabled: true));
client.StartHeartbeat(TimeSpan.FromSeconds(10));
```

RTDS crypto prices:

```csharp
var url = PolymarketValues.RtdsWebsocketApiUrl;

using var communicator = new PolymarketWebsocketCommunicator(url);
using var client = new PolymarketWebsocketClient(communicator);

client.Streams.RtdsCryptoPriceStream.Subscribe(price =>
{
    Console.WriteLine($"{price.Symbol}: {price.Value}");
});

await communicator.Start();

client.Send(RtdsSubscriptionRequest.Subscribe(RtdsSubscription.CryptoPrices("btcusdt", "ethusdt")));
client.StartHeartbeat(TimeSpan.FromSeconds(5));
```

User channel:

```csharp
var auth = new UserAuth("api-key", "secret", "passphrase");
var url = PolymarketValues.UserWebsocketApiUrl;

using var communicator = new PolymarketWebsocketCommunicator(url);
using var client = new PolymarketWebsocketClient(communicator);

client.Streams.UserOrderStream.Subscribe(order =>
{
    Console.WriteLine($"{order.Status} {order.Side} order {order.Id}");
});

await communicator.Start();

client.Send(new UserSubscriptionRequest(auth));
client.StartHeartbeat(TimeSpan.FromSeconds(10));
```

More usage examples:

- integration tests ([link](test_integration/Polymarket.Client.Websocket.Tests.Integration))
- console sample ([link](test_integration/Polymarket.Client.Websocket.Sample/Program.cs))

### API coverage

#### CLOB market channel

| Event | Covered |
|-------|:-------:|
| `book` | yes |
| `price_change` | yes |
| `last_trade_price` | yes |
| `tick_size_change` | yes |
| `best_bid_ask` | yes |
| `new_market` | yes |
| `market_resolved` | yes |

#### CLOB user channel

| Event | Covered |
|-------|:-------:|
| `order` | yes |
| `trade` | yes |

#### Sports channel

| Event | Covered |
|-------|:-------:|
| sports result update | yes |
| server `ping` auto-response | yes |

#### RTDS

| Topic | Covered |
|-------|:-------:|
| `crypto_prices` | yes |
| `crypto_prices_chainlink` | yes |
| `equity_prices` live update | yes |
| `equity_prices` historical snapshot | yes |
| `comments` | yes |

### Reconnecting

There is a built-in reconnection which invokes after 1 minute (default) of not receiving any messages from the server. It is possible to configure that timeout via `communicator.ReconnectTimeout`. There is also a stream `ReconnectionHappened` which sends information about the reconnection type.

You need to resubscribe after reconnection happens. Subscribe to `communicator.ReconnectionHappened` and send the same subscription request again from that handler.

Polymarket expects client heartbeats:

- market and user channels: send `PING` every 10 seconds
- RTDS: send `PING` every 5 seconds
- sports: server sends `ping`; this client automatically responds with `pong` by default

Use `client.StartHeartbeat(...)` for market, user, and RTDS channels.

### Backtesting

The library is prepared for backtesting and data collection. The dependency between `Client` and `Communicator` is via abstraction `IPolymarketCommunicator`. There are two communicator implementations:

- `PolymarketWebsocketCommunicator` - realtime communication with Polymarket websocket APIs
- `PolymarketFileCommunicator` - simulated communication, raw data are loaded from files and streamed

Usage:

```csharp
var communicator = new PolymarketFileCommunicator
{
    FileNames = new[] { "data/polymarket_raw_sample.txt" },
    Delimiter = ";;"
};

using var client = new PolymarketWebsocketClient(communicator);
client.Streams.PriceChangeStream.Subscribe(change =>
{
    // do something with price changes
});

await communicator.Start();
```

### Multi-threading

Observables from Reactive Extensions are single threaded by default. Your code inside subscriptions is called synchronously as soon as a message comes from the websocket API. If your subscription handler takes a long time, it blocks the receiving method, buffers messages, and can eventually lose messages.

Handle messages on another thread when your processing is expensive:

```csharp
client
    .Streams
    .PriceChangeStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Subscribe(change => { /* process */ });
```

If you need parallel processing while preserving order for a stream, use synchronization:

```csharp
private static readonly object Gate = new object();

client
    .Streams
    .PriceChangeStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Synchronize(Gate)
    .Subscribe(change => { /* process */ });
```

**Pull Requests are welcome!**
