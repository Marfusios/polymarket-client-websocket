using System;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polymarket.Client.Websocket.Communicator;
using Polymarket.Client.Websocket.Json;
using Polymarket.Client.Websocket.Responses;
using Polymarket.Client.Websocket.Validations;
using Websocket.Client;

namespace Polymarket.Client.Websocket.Client
{
    /// <summary>
    /// Polymarket websocket client.
    /// Send subscription requests and subscribe to <see cref="Streams"/>.
    /// </summary>
    public class PolymarketWebsocketClient : IDisposable
    {
        private readonly ILogger _logger;
        private readonly IPolymarketCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;
        private readonly PolymarketMessageHandler _messageHandler;
        private IDisposable _heartbeatSubscription;

        /// <summary>
        /// Create a Polymarket websocket client.
        /// </summary>
        public PolymarketWebsocketClient(IPolymarketCommunicator communicator, ILogger<PolymarketWebsocketClient>? logger = null)
        {
            PolyValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _logger = logger ?? NullLogger<PolymarketWebsocketClient>.Instance;
            _messageHandler = new PolymarketMessageHandler(Streams, _logger);
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        /// <summary>
        /// Provided message streams.
        /// </summary>
        public PolymarketClientStreams Streams { get; } = new PolymarketClientStreams();

        /// <summary>
        /// Expose logger for this client.
        /// </summary>
        public ILogger Logger => _logger;

        /// <summary>
        /// Whether to automatically respond with "pong" to server "ping" messages.
        /// Enabled by default for the sports channel.
        /// </summary>
        public bool AutoRespondToPing { get; set; } = true;

        /// <summary>
        /// Cleanup everything.
        /// </summary>
        public void Dispose()
        {
            StopHeartbeat();
            _messageReceivedSubscription?.Dispose();
        }

        /// <summary>
        /// Serializes request and sends it through websocket communicator.
        /// </summary>
        public bool Send<T>(T request)
        {
            try
            {
                PolyValidations.ValidateInput(request, nameof(request));

                var serialized = PolymarketJsonSerializer.Serialize(request);
                return _communicator.Send(serialized);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while sending message '{request}'. Error: {error}", request, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Send a raw text message.
        /// </summary>
        public bool SendRaw(string message)
        {
            try
            {
                PolyValidations.ValidateNotEmpty(message, nameof(message));
                return _communicator.Send(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while sending raw message '{message}'. Error: {error}", message, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Send client heartbeat PING.
        /// </summary>
        public bool SendPing()
        {
            return SendRaw("PING");
        }

        /// <summary>
        /// Send sports-channel heartbeat response.
        /// </summary>
        public bool SendPong()
        {
            return SendRaw("pong");
        }

        /// <summary>
        /// Start sending PING heartbeats on an interval.
        /// Market and user channels expect 10 seconds, RTDS expects 5 seconds.
        /// </summary>
        public void StartHeartbeat(TimeSpan interval)
        {
            StopHeartbeat();
            _heartbeatSubscription = Observable.Interval(interval).Subscribe(_ => SendPing());
        }

        /// <summary>
        /// Stop sending automatic heartbeats.
        /// </summary>
        public void StopHeartbeat()
        {
            _heartbeatSubscription?.Dispose();
            _heartbeatSubscription = null;
        }

        private void HandleMessage(ResponseMessage message)
        {
            try
            {
                var formatted = (message.Text ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(formatted))
                {
                    return;
                }

                Streams.RawMessageSubject.OnNext(formatted);

                if (string.Equals(formatted, "ping", StringComparison.OrdinalIgnoreCase))
                {
                    Streams.PingSubject.OnNext(new PingResponse(formatted));
                    if (AutoRespondToPing)
                    {
                        SendPong();
                    }

                    return;
                }

                _messageHandler.HandleMessage(formatted);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while receiving message, error: {error}", e.Message);
            }
        }
    }
}
