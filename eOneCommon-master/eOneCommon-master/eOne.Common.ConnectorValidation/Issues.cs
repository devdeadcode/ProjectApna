using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.ConnectorValidation
{
    public class Issues
    {

        public Issues()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public enum IssueStatus
        {
            OK,
            Warnings,
            Errors,
            ErrorsAndWarnings
        }

        public DataConnectorEntity Entity { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }

        public IssueStatus Status
        {
            get
            {
                if (Errors.Count > 0 && Warnings.Count > 0) return IssueStatus.ErrorsAndWarnings;
                if (Errors.Count > 0) return IssueStatus.Errors;
                if (Warnings.Count > 0) return IssueStatus.Warnings;
                return IssueStatus.OK;
            }
        }

    }
}
