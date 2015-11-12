namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctWarehouse : IntacctBase
    {

        #region Default properties

        [FieldSettings("Location ID", DefaultField = true, KeyNumber = 1)]
        public string LOCATIONID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Manager ID")]
        public string MANAGERID { get; set; }

        [FieldSettings("Used in GL", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool USEDINGL { get; set; }

        [FieldSettings("Parent location ID", ApiName = "PARENT.LOCATIONID")]
        public string PARENT_LOCATIONID { get; set; }

        [FieldSettings("Manager name", ApiName = "MANAGER.NAME")]
        public string MANAGER_NAME { get; set; }

        [FieldSettings("Contact name", ApiName = "CONTACTINFO.CONTACTNAME")]
        public string CONTACTINFO_CONTACTNAME { get; set; }

        [FieldSettings("Ship to contact name", ApiName = "SHIPTO.CONTACTNAME")]
        public string SHIPTO_CONTACTNAME { get; set; }

        #endregion

    }
}
