using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChat : DataConnectorEntityModel
    {

        #region Enums

        public enum ZopimChatType
        {
            chat,
            offline_msg
        }
        public enum ZopimChatStartedBy
        {
            visitor,
            agent,
            trigger
        }
        public enum ZopimChatRating
        {
            good
        }

        #endregion

        #region Default properties

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChatType))]
        public ZopimChatType type { get; set; }

        [FieldSettings("Started by", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChatStartedBy))]
        public ZopimChatStartedBy started_by { get; set; }

        [FieldSettings("Visitor name", DefaultField = true, SearchPriority = 5)]
        public string visitor_display_name => visitor.display_name;

        [FieldSettings("Visitor email", DefaultField = true, SearchPriority = 4, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string visitor_email => visitor.email;

        [FieldSettings("Agents", DefaultField = true, SearchPriority = 3)]
        public string agent_name_list => string.Join(", ", agent_names);

        #endregion

        #region Properties

        [FieldSettings("Duration", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int duration { get; set; }

        [FieldSettings("Triggered", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool triggered { get; set; }

        [FieldSettings("Missed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool missed { get; set; }

        [FieldSettings("Unread", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool unread { get; set; }

        [FieldSettings("Triggered response", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool triggered_response { get; set; }

        [FieldSettings("Triggered", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZopimChatRating))]
        public ZopimChatRating rating { get; set; }

        [FieldSettings("Comment", SearchPriority = 2)]
        public string comment { get; set; }

        #endregion

        #region Hidden properties

        public ZopimChatMessageCount count { get; set; }
        public ZopimVisitor visitor { get; set; }
        public List<int> agent_ids { get; set; }
        public List<string> agent_names { get; set; }
        public List<ZopimChatMessage> history { get; set; }
        public ZopimChatResponseTime response_time { get; set; }
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public List<string> tags { get; set; }
        public ZopimChatSession session { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string tag_list => string.Join(", ", tags);

        [FieldSettings("Maximum response time", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int response_time_max => response_time.max;

        [FieldSettings("Average response time", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int response_time_avg => response_time.avg;

        [FieldSettings("First response time", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int response_time_first => response_time.first;

        [FieldSettings("Number of visitor messages", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int count_visitor => count.visitor;

        [FieldSettings("Total number of messages", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int count_total => count.total;

        [FieldSettings("Number of agent messages", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int count_agent => count.agent;

        [FieldSettings("Visitor created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime visitor_created_date => visitor.created.Date;

        [FieldSettings("Visitor notes")]
        public string visitor_notes => visitor.notes;

        [FieldSettings("Visitor phone", SearchPriority = 2)]
        public string visitor_phone => visitor.phone;

        [FieldSettings("Visitor banned", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool visitor_banned => visitor.banned == 1;

        [FieldSettings("Session city", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public string session_city => session.city;

        [FieldSettings("Session start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime session_end_date => session.end_date.Date;

        [FieldSettings("Session start time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime session_end_time => Time(session.end_date);

        [FieldSettings("Session IP address")]
        public string session_ip => session.ip;

        [FieldSettings("Session region")]
        public string session_region => session.region;

        [FieldSettings("Session platform")]
        public string session_platform => session.platform;

        [FieldSettings("Session user agent")]
        public string session_user_agent => session.user_agent;

        [FieldSettings("Session country code")]
        public string session_country_code => session.country_code;

        [FieldSettings("Session country name")]
        public string session_country_name => session.country_name;

        [FieldSettings("Session start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime session_start_date => session.start_date.Date;

        [FieldSettings("Session start time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime session_start_time => Time(session.start_date);

        [FieldSettings("Session browser")]
        public string session_browser => session.browser;

        #endregion

    }
}