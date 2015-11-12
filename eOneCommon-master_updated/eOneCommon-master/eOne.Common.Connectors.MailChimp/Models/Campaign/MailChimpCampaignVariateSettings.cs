using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignVariateSettings
    {

        public string winning_combination_id { get; set; }
        public string winning_campaign_id { get; set; }
        public List<string> subject_lines { get; set; }
        public List<DateTime> send_times { get; set; }
        public List<string> from_names { get; set; }
        public List<string> reply_to_addresses { get; set; }
        public List<string> contents { get; set; }
        public List<MailChimpCampaignVariateCombination> combinations { get; set; }

        public string winning_subject_line
        {
            get
            {
                foreach (var combination in combinations.Where(combination => combination.id == winning_combination_id))
                {
                    if (subject_lines.Count > combination.subject_line) return subject_lines[combination.subject_line];
                }
                return string.Empty;
            }
        }
        public DateTime? winning_send_time
        {
            get
            {
                foreach (var combination in combinations.Where(combination => combination.id == winning_combination_id))
                {
                    if (send_times.Count > combination.send_time) return send_times[combination.send_time];
                }
                return null;
            }
        }
        public string winning_from_name
        {
            get
            {
                foreach (var combination in combinations.Where(combination => combination.id == winning_combination_id))
                {
                    if (from_names.Count > combination.from_name) return from_names[combination.from_name];
                }
                return string.Empty;
            }
        }
        public string winning_reply_to_address
        {
            get
            {
                foreach (var combination in combinations.Where(combination => combination.id == winning_combination_id))
                {
                    if (reply_to_addresses.Count > combination.reply_to) return reply_to_addresses[combination.reply_to];
                }
                return string.Empty;
            }
        }
        public string winning_contents
        {
            get
            {
                foreach (var combination in combinations.Where(combination => combination.id == winning_combination_id))
                {
                    if (contents.Count > combination.content_description) return contents[combination.content_description];
                }
                return string.Empty;
            }
        }
        public int number_of_combinations => combinations?.Count ?? 0;
        public int number_of_subject_lines => subject_lines?.Count ?? 0;
        public int number_of_send_times => send_times?.Count ?? 0;
        public int number_of_from_names => from_names?.Count ?? 0;
        public int number_of_reply_to_addresses => reply_to_addresses?.Count ?? 0;
        public int number_of_contents => contents?.Count ?? 0;

    }
}