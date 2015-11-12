using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctLocation : IntacctBase
    {

        #region Default properties

        [FieldSettings("Location ID", DefaultField = true, KeyNumber = 1)]
        public string LOCATIONID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Parent ID")]
        public string PARENTID { get; set; }

        [FieldSettings("Manager ID")]
        public string SUPERVISORNAME { get; set; }

        [FieldSettings("Manager name")]
        public string SUPERVISORID { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? STARTDATE { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ENDDATE { get; set; }

        [FieldSettings("Location contact", ApiName = "CONTACTINFO.CONTACTNAME")]
        public string CONTACTINFO_CONTACTNAME { get; set; }

        [FieldSettings("Ship to contact", ApiName = "SHIPTO.CONTACTNAME")]
        public string SHIPTO_CONTACTNAME { get; set; }

        #endregion

    }
}