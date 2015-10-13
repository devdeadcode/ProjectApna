using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatMessage : DataConnectorEntityModel
    {

        public enum ZopimChatHistoryType
        {
            chat_msg,
            chat_memberjoin,
            chat_memberleave
        }

        public string name { get; set; }
        public ZopimChatHistoryType chat_type => (ZopimChatHistoryType)Enum.Parse(typeof(ZopimChatHistoryType), type.Replace('.', '_'), true);
        public string msg { get; set; }
        public DateTime timestamp_date => timestamp.Date;

        public DateTime timestamp_time => Time(timestamp);

        public string channel { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }

    }
}