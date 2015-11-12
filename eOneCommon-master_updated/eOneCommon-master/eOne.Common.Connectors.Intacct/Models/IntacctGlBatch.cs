using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctGlBatch
    {

        public string BATCHNO { get; set; }
        public string BATCH_TITLE { get; set; }
        public string JOURNAL { get; set; }
        public string STATE { get; set; }
        public DateTime? BATCH_DATE { get; set; }

        public decimal? BALANCE { get; set; }

    }
}

//	"ADJ": "F",
//	"MODULE": "2.GL",
//	"CHILDENTITY": null,
//	"REFERENCENO": "4.10 tb",
//	"REVERSED": null,
//	"REVERSEDFROM": null,
//	"PRBATCHRECTYPE": null,
//	"MODIFIED": "10\/16\/2010 14:36:10",
//	"MODIFIEDBYID": "admin",
//	"BASELOCATION": null,
//	"BASELOCATION_NO": null,
//	"BASELOCATION_NAME": null,
//	"USERINFO.LOGINID": "admin",
