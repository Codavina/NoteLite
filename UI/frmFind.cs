using NoteLite.Services;
using System;
using System.Windows.Forms;

namespace NoteLite.Edit
{
    public partial class frmFind : Form
    {

        private readonly IFindService _findService;

        public frmFind(IFindService findService)
        {
            InitializeComponent();
            _findService = findService;
        }

        private void btnFindNext_Click_1(object sender, EventArgs e)
        {
            bool found = _findService.FindNext(txtToFind.Text);

            if (!found)
            {
                MessageBox.Show($"Cannot find ({txtToFind.Text}).", "Find",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
