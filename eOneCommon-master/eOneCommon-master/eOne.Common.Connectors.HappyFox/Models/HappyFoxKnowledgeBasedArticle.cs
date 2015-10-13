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
        HappyFoxArticle know_based_article = new HappyFoxArticle();

        [FieldSettings("Title", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Contents")]
        public string content => know_based_article.contents;

        [FieldSettings("Article ID")]
        public int id => know_based_article.id;

        [FieldSettings("Views")]
        public int views => know_based_article.views;

        [FieldSettings("Section", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Section description")]
        public string sec_description { get; set; }

        [FieldSettings("Last updated date")]
        public string last_updated_date => know_based_article.last_updated_date;

        [FieldSettings("Last updated time")]
        public string last_updated_time => know_based_article.last_updated_time;

        #region Hidden properties
        public List<HappyFoxCategory> categories { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Categories", DefaultField = true)]
        public string category_names {
            get{
                string csv = null;
                for(int i = 0; i<categories.Count; i++)
                {
                    if (categories[i].name != null)
                    {
                        csv = string.Join(",", categories.Select(x => x.name));
                    }
                }
                return csv;
                    
            }
        }
        #endregion
    }
}
