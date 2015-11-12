using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.HappyFox.Models
{
    //public class Contact
    //{
    //    public PageInfo page_info { get; set; }
    //    public List<HappyFoxContact> data { get; set; }
    //}

    //public class PageInfo
    //{
    //    public int count { get; set; }
    //}

    public class HappyFoxContact : ConnectorEntityModel
    {
        [FieldSettings("Contact ID")]
        public int id { get; set; }

        [FieldSettings("Email", DefaultField = true)]
        public string email { get; set; }

        public List<string> custom_fields { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        public HappyFoxTicketSummary ticket_summary { get; set; }

        [FieldSettings("Number of tickets")]
        public int number_of_tickets
        {
            get
            {
                try
                {
                    return ticket_summary.num_of_tickets;
                }
                catch{return 0;}

            }
            set { }
        }

        #region Hidden properties
        public List<HappyFoxStatus> status { get; set; }

        public List<HappyFoxContact> data { get; set; }

        public HappyFoxContactCollection contactCollection { get; set; }
        #endregion

        #region Calcualtions
        [FieldSettings("Contacts with pending tickets")]
        public int num_of_pending_tickets
        {
            get
            {
                try
                {
                    return status.Count(t => t.name == "Pending");
                }
                catch { return 0; }
                
            }

            set { }
        }

        #endregion

    }
}
