using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polymarket.Client.Websocket;
using Polymarket.Client.Websocket.Client;
using Polymarket.Client.Websocket.Requests;
using Polymarket.Client.Websocket.Websockets;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace Polymarket.Client.Websocket.Sample
{
    internal static class Program
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        private static async Task Main()
        {
            var logger = InitLogging();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            Console.WriteLine("|========================|");
            Console.WriteLine("|   POLYMARKET CLIENT    |");
            Console.WriteLine("|========================|");
            Console.WriteLine();

            Log.Debug("====================================");
            Log.Debug("              STARTING              ");
            Log.Debug("====================================");

            var assetIds = (Environment.GetEnvironmentVariable("POLYMARKET_ASSET_IDS") ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

            if (assetIds.Length > 0)
            {
                await RunMarketChannel(logger, assetIds);
            }
            else
            {
                await RunRtdsChannel(logger);
            }

            Log.Debug("====================================");
            Log.Debug("              STOPPING              ");
            Log.Debug("====================================");
            Log.CloseAndFlush();
        }

        private static async Task RunMarketChannel(SerilogLoggerFactory logger, string[] assetIds)
        {
            using var communicator = new PolymarketWebsocketCommunicator(
                PolymarketValues.MarketWebsocketApiUrl,
                logger.CreateLogger<PolymarketWebsocketCommunicator>());
            communicator.Name = "Polymarket-market-1";
            communicator.ReconnectTimeout = TimeSpan.FromSeconds(30);

            using var client = new PolymarketWebsocketClient(communicator, logger.CreateLogger<PolymarketWebsocketClient>());
            SubscribeToMarketStreams(client);

            communicator.ReconnectionHappened.Subscribe(info =>
            {
                Log.Information("Reconnection happened, type: {type}, resubscribing...", info.Type);
                client.Send(new MarketSubscriptionRequest(assetIds, customFeatureEnabled: true));
            });

            await communicator.Start();
            client.Send(new MarketSubscriptionRequest(assetIds, customFeatureEnabled: true));
            client.StartHeartbeat(TimeSpan.FromSeconds(10));

            ExitEvent.WaitOne();
        }

        private static async Task RunRtdsChannel(SerilogLoggerFactory logger)
        {
            using var communicator = new PolymarketWebsocketCommunicator(
                PolymarketValues.RtdsWebsocketApiUrl,
                logger.CreateLogger<PolymarketWebsocketCommunicator>());
            communicator.Name = "Polymarket-rtds-1";
            communicator.ReconnectTimeout = TimeSpan.FromSeconds(30);

            using var client = new PolymarketWebsocketClient(communicator, logger.CreateLogger<PolymarketWebsocketClient>());
            SubscribeToRtdsStreams(client);

            var subscription = RtdsSubscriptionRequest.Subscribe(RtdsSubscription.CryptoPrices("btcusdt", "ethusdt"));
            communicator.ReconnectionHappened.Subscribe(info =>
            {
                Log.Information("Reconnection happened, type: {type}, resubscribing...", info.Type);
                client.Send(subscription);
            });

            await communicator.Start();
            client.Send(subscription);
            client.StartHeartbeat(TimeSpan.FromSeconds(5));

            Log.Information("POLYMARKET_ASSET_IDS is not set, so the sample is using RTDS crypto prices.");
            ExitEvent.WaitOne();
        }

        private static void SubscribeToMarketStreams(PolymarketWebsocketClient client)
        {
            client.Streams.OrderBookStream.Subscribe(book =>
                Log.Information("Book {asset}: bids={bids}, asks={asks}, hash={hash}",
                    book.AssetId,
                    book.Bids?.Length ?? 0,
                    book.Asks?.Length ?? 0,
                    book.Hash));

            client.Streams.PriceChangeStream.Subscribe(change =>
                Log.Information("Price changes for {market}: {count}", change.Market, change.PriceChanges?.Length ?? 0));

            client.Streams.LastTradePriceStream.Subscribe(trade =>
                Log.Information("Trade {asset}: {side} {size} @ {price}", trade.AssetId, trade.Side, trade.Size, trade.Price));

            client.Streams.BestBidAskStream.Subscribe(quote =>
                Log.Information("BBA {asset}: bid={bid}, ask={ask}, spread={spread}", quote.AssetId, quote.BestBid, quote.BestAsk, quote.Spread));
        }

        private static void SubscribeToRtdsStreams(PolymarketWebsocketClient client)
        {
            client.Streams.RtdsCryptoPriceStream.Subscribe(price =>
                Log.Information("RTDS {symbol}: {value}", price.Symbol, price.Value));

            client.Streams.RtdsMessageStream.Subscribe(message =>
                Log.Debug("RTDS raw topic={topic}, type={type}", message.Topic, message.Type));
        }

        private static SerilogLoggerFactory InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? ".";
            var logPath = Path.Combine(executingDir, "logs", "verbose.log");
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console(LogEventLevel.Debug, outputTemplate:
                    "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Logger = logger;
            return new SerilogLoggerFactory(logger);
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Warning("Exiting process");
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Warning("Unloading process");
            ExitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            Log.Warning("Canceling process");
            consoleCancelEventArgs.Cancel = true;
            ExitEvent.Set();
        }
    }
}
