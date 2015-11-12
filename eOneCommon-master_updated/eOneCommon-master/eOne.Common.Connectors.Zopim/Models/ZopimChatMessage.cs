using System;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatMessage : ConnectorEntityModel
    {

        public enum ZopimChatHistoryType
        {
            chat_msg,
            chat_memberjoin,
            chat_memberleave
        }

        public string name { get; set; }
        public ZopimChatHistoryType chat_type => ParseEnum<ZopimChatHistoryType>(type.Replace('.', '_'));
        public string msg { get; set; }

        public string channel { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime timestamp_time => timestamp;

    }
}