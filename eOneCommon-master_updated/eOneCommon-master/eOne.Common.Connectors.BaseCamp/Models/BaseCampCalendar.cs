using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampCalendar
    {

        public int id { get; set; }
        public string name { get; set; }
        public DateTime updated_at { get; set; }
        public string color { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }

    }
}