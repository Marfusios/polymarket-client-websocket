using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Polymarket.Client.Websocket.Responses;
using Polymarket.Client.Websocket.Responses.Market;
using Polymarket.Client.Websocket.Responses.Rtds;
using Polymarket.Client.Websocket.Responses.Sports;
using Polymarket.Client.Websocket.Responses.User;

namespace Polymarket.Client.Websocket.Client
{
    /// <summary>
    /// All provided streams from Polymarket websocket APIs.
    /// </summary>
    public class PolymarketClientStreams
    {
        internal readonly Subject<string> RawMessageSubject = new Subject<string>();
        internal readonly Subject<PingResponse> PingSubject = new Subject<PingResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();

        internal readonly Subject<OrderBookSnapshotResponse> OrderBookSubject = new Subject<OrderBookSnapshotResponse>();
        internal readonly Subject<PriceChangeResponse> PriceChangeSubject = new Subject<PriceChangeResponse>();
        internal readonly Subject<LastTradePriceResponse> LastTradePriceSubject = new Subject<LastTradePriceResponse>();
        internal readonly Subject<TickSizeChangeResponse> TickSizeChangeSubject = new Subject<TickSizeChangeResponse>();
        internal readonly Subject<BestBidAskResponse> BestBidAskSubject = new Subject<BestBidAskResponse>();
        internal readonly Subject<NewMarketResponse> NewMarketSubject = new Subject<NewMarketResponse>();
        internal readonly Subject<MarketResolvedResponse> MarketResolvedSubject = new Subject<MarketResolvedResponse>();

        internal readonly Subject<OrderEventResponse> UserOrderSubject = new Subject<OrderEventResponse>();
        internal readonly Subject<TradeEventResponse> UserTradeSubject = new Subject<TradeEventResponse>();

        internal readonly Subject<SportsResultResponse> SportsResultSubject = new Subject<SportsResultResponse>();

        internal readonly Subject<RtdsMessage> RtdsMessageSubject = new Subject<RtdsMessage>();
        internal readonly Subject<RtdsPricePayload> RtdsCryptoPriceSubject = new Subject<RtdsPricePayload>();
        internal readonly Subject<RtdsPricePayload> RtdsEquityPriceSubject = new Subject<RtdsPricePayload>();
        internal readonly Subject<RtdsEquitySnapshotPayload> RtdsEquitySnapshotSubject = new Subject<RtdsEquitySnapshotPayload>();
        internal readonly Subject<RtdsCommentPayload> RtdsCommentSubject = new Subject<RtdsCommentPayload>();

        /// <summary>
        /// Raw text messages received from websocket.
        /// </summary>
        public IObservable<string> RawMessageStream => RawMessageSubject.AsObservable();

        /// <summary>
        /// Server ping stream, mainly used by the sports channel.
        /// </summary>
        public IObservable<PingResponse> PingStream => PingSubject.AsObservable();

        /// <summary>
        /// Response stream to client heartbeat pings.
        /// </summary>
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();

        /// <summary>
        /// Full orderbook snapshots.
        /// </summary>
        public IObservable<OrderBookSnapshotResponse> OrderBookStream => OrderBookSubject.AsObservable();

        /// <summary>
        /// Orderbook price level updates.
        /// </summary>
        public IObservable<PriceChangeResponse> PriceChangeStream => PriceChangeSubject.AsObservable();

        /// <summary>
        /// Last trade price updates.
        /// </summary>
        public IObservable<LastTradePriceResponse> LastTradePriceStream => LastTradePriceSubject.AsObservable();

        /// <summary>
        /// Tick size changes.
        /// </summary>
        public IObservable<TickSizeChangeResponse> TickSizeChangeStream => TickSizeChangeSubject.AsObservable();

        /// <summary>
        /// Best bid and ask updates.
        /// </summary>
        public IObservable<BestBidAskResponse> BestBidAskStream => BestBidAskSubject.AsObservable();

        /// <summary>
        /// New market lifecycle events.
        /// </summary>
        public IObservable<NewMarketResponse> NewMarketStream => NewMarketSubject.AsObservable();

        /// <summary>
        /// Market resolved lifecycle events.
        /// </summary>
        public IObservable<MarketResolvedResponse> MarketResolvedStream => MarketResolvedSubject.AsObservable();

        /// <summary>
        /// Authenticated order updates.
        /// </summary>
        public IObservable<OrderEventResponse> UserOrderStream => UserOrderSubject.AsObservable();

        /// <summary>
        /// Authenticated trade updates.
        /// </summary>
        public IObservable<TradeEventResponse> UserTradeStream => UserTradeSubject.AsObservable();

        /// <summary>
        /// Sports result updates.
        /// </summary>
        public IObservable<SportsResultResponse> SportsResultStream => SportsResultSubject.AsObservable();

        /// <summary>
        /// Generic RTDS message stream.
        /// </summary>
        public IObservable<RtdsMessage> RtdsMessageStream => RtdsMessageSubject.AsObservable();

        /// <summary>
        /// RTDS crypto price updates.
        /// </summary>
        public IObservable<RtdsPricePayload> RtdsCryptoPriceStream => RtdsCryptoPriceSubject.AsObservable();

        /// <summary>
        /// RTDS equity live price updates.
        /// </summary>
        public IObservable<RtdsPricePayload> RtdsEquityPriceStream => RtdsEquityPriceSubject.AsObservable();

        /// <summary>
        /// RTDS equity snapshot updates.
        /// </summary>
        public IObservable<RtdsEquitySnapshotPayload> RtdsEquitySnapshotStream => RtdsEquitySnapshotSubject.AsObservable();

        /// <summary>
        /// RTDS comment events.
        /// </summary>
        public IObservable<RtdsCommentPayload> RtdsCommentStream => RtdsCommentSubject.AsObservable();
    }
}
