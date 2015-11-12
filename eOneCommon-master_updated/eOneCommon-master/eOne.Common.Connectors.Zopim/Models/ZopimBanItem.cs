namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBanItem : ConnectorEntityModel
    {

        public ZopimBanItem(ZopimBannedVisitor zopim_visitor)
        {
            visitor = zopim_visitor;
        }
        public ZopimBanItem(ZopimBannedIp zopim_ip)
        {
            ip = zopim_ip;
        }

        #region Enums

        public enum ZopimBanItemType
        {
            none,
            visitor,
            ip
        }

        #endregion

        #region Hidden properties

        public ZopimBannedVisitor visitor { get; set; }
        public ZopimBannedIp ip { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZopimBanItemType))]
        public ZopimBanItemType type
        {
            get
            {
                if (visitor != null) return ZopimBanItemType.visitor;
                return ip != null ? ZopimBanItemType.ip : ZopimBanItemType.none;
            }
        }

        [FieldSettings("IP address", DefaultField = true)]
        public string ip_address => ip != null ? ip.ip_address : string.Empty;

        [FieldSettings("Vistor name", DefaultField = true, SearchPriority = 2)]
        public string visitor_name => visitor != null ? visitor.visitor_name : string.Empty;

        [FieldSettings("Reason", DefaultField = true)]
        public string reason
        {
            get
            {
                if (visitor != null) return visitor.reason;
                return ip != null ? ip.reason : string.Empty;
            }
        }

        #endregion

    }
}
