using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    class HappyFoxKnowledgeBasedArticle : DataConnectorEntityModel
    {
        //public HappyFoxArticle know_based_article { get; set; }
        public List<HappyFoxArticle> articles { get; set; }

        [FieldSettings("Title", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Contents")]
        public string content
        {
            get
            {
                var cnt = string.Empty;
                foreach (var val in articles) cnt = val.contents;
                return cnt;
            }
            set { }
        }

        [FieldSettings("Article ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id
        {
            get
            {
                var iD = 0;
                foreach (var val in articles) iD = val.id;
                return iD;
            }
            set { }
        }

        [FieldSettings("Views", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int views
        {
            get
            {
                var vw = 0;
                foreach (var val in articles) vw = val.views;
                return vw;
            }
            set { }
        }

        [FieldSettings("Section", DefaultField = true)]
        public string sec_name
        {
            get
            {
                string vw = null;
                foreach (var val in articles) vw = val.section_name;
                return vw;
            }
            set { }
        }

        [FieldSettings("Section description")]
        public string description { get; set; }

        [FieldSettings("Last updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string last_updated_date
        {
            get
            {
                var lastDate = string.Empty;
                foreach (var val in articles) lastDate = val.last_updated_date;
                return lastDate;
            }
        }

        [FieldSettings("Last updated time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string last_updated_time
        {
            get
            {
                var lastTime = string.Empty;
                foreach (var val in articles) lastTime = val.last_updated_time;
                return lastTime;
            }
        }

        #region Hidden properties
        public List<HappyFoxCategory> categories { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Categories")]
        public string category_names
        {
            get
            {
                string csv = null;
                foreach (var t in categories.Where(t => t.name != null))
                {
                    csv = string.Join(",", categories.Select(x => x.name));
                }
                return csv;

            }
        }
        #endregion
    }
}
