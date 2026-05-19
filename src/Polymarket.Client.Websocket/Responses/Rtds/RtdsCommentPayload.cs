using System;
using Newtonsoft.Json;

namespace Polymarket.Client.Websocket.Responses.Rtds
{
    /// <summary>
    /// RTDS comment event payload.
    /// </summary>
    public class RtdsCommentPayload
    {
        /// <summary>
        /// Comment body.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Comment ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Parent comment ID.
        /// </summary>
        [JsonProperty("parentCommentID")]
        public string ParentCommentId { get; set; }

        /// <summary>
        /// Parent entity ID.
        /// </summary>
        [JsonProperty("parentEntityID")]
        public long? ParentEntityId { get; set; }

        /// <summary>
        /// Parent entity type.
        /// </summary>
        [JsonProperty("parentEntityType")]
        public string ParentEntityType { get; set; }

        /// <summary>
        /// Author profile.
        /// </summary>
        [JsonProperty("profile")]
        public RtdsProfile Profile { get; set; }

        /// <summary>
        /// Reaction count.
        /// </summary>
        [JsonProperty("reactionCount")]
        public int? ReactionCount { get; set; }

        /// <summary>
        /// Reply address.
        /// </summary>
        [JsonProperty("replyAddress")]
        public string ReplyAddress { get; set; }

        /// <summary>
        /// Report count.
        /// </summary>
        [JsonProperty("reportCount")]
        public int? ReportCount { get; set; }

        /// <summary>
        /// User address.
        /// </summary>
        [JsonProperty("userAddress")]
        public string UserAddress { get; set; }
    }
}
