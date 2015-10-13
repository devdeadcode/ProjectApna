using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatHistory : DataConnectorEntityModel
    {

        public ZopimChatHistory(ZopimChatMessage zopim_message, ZopimChat zopim_chat)
        {
            message = zopim_message;
            chat = zopim_chat;
        }

        #region Hidden properties

        public ZopimChatMessage message { get; set; }
        public ZopimChat chat { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Name", DefaultField = true, SearchPriority = 5)]
        public string message_name => message.name;

        [FieldSettings("Message type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChatMessage.ZopimChatHistoryType))]
        public ZopimChatMessage.ZopimChatHistoryType message_type => message.chat_type;

        [FieldSettings("Message", DefaultField = true, SearchPriority = 3)]
        public string message_msg => message.msg;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime message_timestamp_date => message.timestamp_date;

        [FieldSettings("Time", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime message_timestamp_time => message.timestamp_time;

        [FieldSettings("Channel", SearchPriority = 2)]
        public string message_channel => message.channel;

        [FieldSettings("Visitor name", SearchPriority = 4)]
        public string chat_visitor_name => chat.visitor_display_name;

        [FieldSettings("Chat type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChat.ZopimChatType))]
        public ZopimChat.ZopimChatType chat_type => chat.type;

        [FieldSettings("Chat started by", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChat.ZopimChatStartedBy))]
        public ZopimChat.ZopimChatStartedBy chat_started_by => chat.started_by;

        [FieldSettings("Visitor email", SearchPriority = 3, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string chat_visitor_email => chat.visitor_email;

        [FieldSettings("Agents")]
        public string chat_agent_name_list => chat.agent_name_list;

        #endregion

    }
}
