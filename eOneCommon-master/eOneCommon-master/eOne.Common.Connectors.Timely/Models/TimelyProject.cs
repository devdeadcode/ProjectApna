using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyProject : DataConnectorEntityModel
    {

        #region Enums

        public enum TimelyProjectBudgetType
        {
            [Description("Time")]
            H,
            [Description("Money")]
            M
        }
        public enum TimelyProjectRateType
        {
            [Description("Project")]
            project,
            [Description("User")]
            user,
            [Description("Non-billable")]
            non_billable
        }

        #endregion

        #region Default parameters

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Client name", DefaultField = true)]
        public string client_name => (client == null) ? string.Empty : client.name;

        [FieldSettings("Number of users", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int users_count => users.Count;

        [FieldSettings("Tags", DefaultField = true)]
        public string hashtags
        {
            get
            {
                return CommaSeparatedValues(tags.Select(tag => $"#{tag.name}").ToList());
            }
        }

        #endregion

        #region Parameters

        [FieldSettings("Active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Budget", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? budget { get; set; }

        [FieldSettings("Budget type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(TimelyProjectBudgetType))]
        public TimelyProjectBudgetType? budget_type { get; set; }

        [FieldSettings("Billable", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool billable { get; set; }

        [FieldSettings("Hourly rate", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal hour_rate { get; set; }

        [FieldSettings("Project ID", KeyNumber = 1, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        #endregion

        #region Hidden parameters
        
        public List<TimelyTag> tags { get; set; }
        public List<TimelyUser> users { get; set; }
        public string rate_type { get; set; }
        public string color { get; set; }
        public int updated_at { get; set; }
        public TimelyCost total_estimated { get; set; }
        public TimelyCost total_logged { get; set; }
        public TimelyClient client { get; set; }
        public int account_id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => DateTime.FromOADate(updated_at);

        [FieldSettings("Rate type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(TimelyProjectRateType))]
        public TimelyProjectRateType rate_type_enum
        {
            get
            {
                var value = rate_type.Replace('-', '_');
                return (TimelyProjectRateType)Enum.Parse(typeof(TimelyProjectRateType), value, true);
            }
        }

        [FieldSettings("Total estimated cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double total_estimated_cost => total_estimated.cost;

        [FieldSettings("Total logged cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double total_logged_cost => total_logged.cost;

        [FieldSettings("Total estimated time")]
        public string total_estimated_time => total_estimated.time;

        [FieldSettings("Total logged time")]
        public string total_logged_time => total_logged.time;

        [FieldSettings("Project link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string project_url => $"https://timelyapp.com/{account_id}/projects/{id}";

        public int client_id => client.id;

        #endregion

    }
}
