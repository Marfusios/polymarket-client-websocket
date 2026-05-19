using System.Runtime.Serialization;
using Newtonsoft.Json;
using Polymarket.Client.Websocket.Json;

namespace Polymarket.Client.Websocket.Enums
{
    /// <summary>
    /// Polymarket market and user websocket event type.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketEventType>))]
    public enum PolymarketEventType
    {
        Unknown = 0,

        [EnumMember(Value = "book")]
        Book,

        [EnumMember(Value = "price_change")]
        PriceChange,

        [EnumMember(Value = "last_trade_price")]
        LastTradePrice,

        [EnumMember(Value = "tick_size_change")]
        TickSizeChange,

        [EnumMember(Value = "best_bid_ask")]
        BestBidAsk,

        [EnumMember(Value = "new_market")]
        NewMarket,

        [EnumMember(Value = "market_resolved")]
        MarketResolved,

        [EnumMember(Value = "order")]
        Order,

        [EnumMember(Value = "trade")]
        Trade
    }

    /// <summary>
    /// Polymarket order side.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketOrderSide>))]
    public enum PolymarketOrderSide
    {
        Unknown = 0,

        [EnumMember(Value = "BUY")]
        Buy,

        [EnumMember(Value = "SELL")]
        Sell
    }

    /// <summary>
    /// User-channel order event type.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketOrderUpdateType>))]
    public enum PolymarketOrderUpdateType
    {
        Unknown = 0,

        [EnumMember(Value = "PLACEMENT")]
        Placement,

        [EnumMember(Value = "UPDATE")]
        Update,

        [EnumMember(Value = "CANCELLATION")]
        Cancellation
    }

    /// <summary>
    /// Polymarket order time in force.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketTimeInForce>))]
    public enum PolymarketTimeInForce
    {
        Unknown = 0,

        [EnumMember(Value = "FOK")]
        FillOrKill,

        [EnumMember(Value = "FAK")]
        ImmediateOrCancel,

        [EnumMember(Value = "GTC")]
        GoodTillCanceled,

        [EnumMember(Value = "GTD")]
        GoodTillDate
    }

    /// <summary>
    /// User-channel order status.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketOrderStatus>))]
    public enum PolymarketOrderStatus
    {
        Unknown = 0,

        [EnumMember(Value = "LIVE")]
        Live,

        [EnumMember(Value = "CANCELED")]
        Canceled,

        [EnumMember(Value = "MATCHED")]
        Matched,

        [EnumMember(Value = "DELAYED")]
        Delayed
    }

    /// <summary>
    /// User-channel trade event type.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketTradeEventType>))]
    public enum PolymarketTradeEventType
    {
        Unknown = 0,

        [EnumMember(Value = "TRADE")]
        Trade
    }

    /// <summary>
    /// User-channel trade status.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketTradeStatus>))]
    public enum PolymarketTradeStatus
    {
        Unknown = 0,

        [EnumMember(Value = "MATCHED")]
        Matched,

        [EnumMember(Value = "MINED")]
        Mined,

        [EnumMember(Value = "CONFIRMED")]
        Confirmed,

        [EnumMember(Value = "RETRYING")]
        Retrying,

        [EnumMember(Value = "FAILED")]
        Failed
    }

    /// <summary>
    /// User-channel trade role.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<PolymarketTradeRole>))]
    public enum PolymarketTradeRole
    {
        Unknown = 0,

        [EnumMember(Value = "TAKER")]
        Taker,

        [EnumMember(Value = "MAKER")]
        Maker
    }

    /// <summary>
    /// RTDS topic.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<RtdsTopic>))]
    public enum RtdsTopic
    {
        Unknown = 0,

        [EnumMember(Value = "crypto_prices")]
        CryptoPrices,

        [EnumMember(Value = "crypto_prices_chainlink")]
        CryptoPricesChainlink,

        [EnumMember(Value = "equity_prices")]
        EquityPrices,

        [EnumMember(Value = "comments")]
        Comments
    }

    /// <summary>
    /// RTDS message type.
    /// </summary>
    [JsonConverter(typeof(PolymarketStringEnumConverter<RtdsMessageType>))]
    public enum RtdsMessageType
    {
        Unknown = 0,

        [EnumMember(Value = "subscribe")]
        Subscribe,

        [EnumMember(Value = "update")]
        Update,

        [EnumMember(Value = "error")]
        Error
    }
}
