using System;
using System.Windows.Forms;

namespace BPDMH.Report
{
    public partial class BPDHReportForm : Form
    {
        public BPDHReportForm()
        {
            InitializeComponent();
        }

        private void BPDHReportForm_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
