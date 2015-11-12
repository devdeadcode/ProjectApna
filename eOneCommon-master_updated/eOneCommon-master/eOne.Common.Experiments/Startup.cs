using System;
using System.Windows.Forms;

namespace eOne.Common.Experiments
{
    public partial class Startup : Form
    {

        private enum ExperimentType
        {
            Summary,
            Compare
        }

        public Startup()
        {
            InitializeComponent();
            foreach (ExperimentType type in Enum.GetValues(typeof(ExperimentType)))
            {
                cboExperiment.Items.Add(type);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            switch ((ExperimentType)cboExperiment.SelectedItem)
            {
                case ExperimentType.Summary:
                    var getSummaryForm = new GetSummary();
                    getSummaryForm.Show();
                    break;
                case ExperimentType.Compare:
                    var compareRecordsForm = new CompareRecords();
                    compareRecordsForm.Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
