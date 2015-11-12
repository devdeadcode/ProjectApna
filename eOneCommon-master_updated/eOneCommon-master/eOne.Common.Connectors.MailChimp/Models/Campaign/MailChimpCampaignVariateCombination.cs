using System;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignVariateCombination
    {

        public string id { get; set; }
        public int subject_line { get; set; }
        public int send_time { get; set; }
        public int from_name { get; set; }
        public int reply_to { get; set; }
        public int content_description { get; set; }
        public int recipients { get; set; }

        public MailChimpCampaignVariateSettings settings { get; set; }

        public string subject_line_value => settings.subject_lines.Count > subject_line ? string.Empty : settings.subject_lines[subject_line];
        public DateTime? send_time_value
        {
            get
            {
                if (settings.send_times.Count > send_time) return null;
                return settings.send_times[send_time];
            }
        } 
        public string from_name_value => settings.from_names.Count > from_name ? string.Empty : settings.from_names[from_name];
        public string reply_to_value => settings.reply_to_addresses.Count > reply_to ? string.Empty : settings.reply_to_addresses[reply_to];
        public string content_description_value => settings.contents.Count > content_description ? string.Empty : settings.contents[content_description];
        public bool is_winner => id == settings.winning_combination_id;

    }
}