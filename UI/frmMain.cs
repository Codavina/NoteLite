using NoteLite.Edit;
using NoteLite.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace NoteLite
{
    public partial class frmMain : Form
    {

        private readonly IFileService _fileService = new FileService();
        private readonly IEditService _editService = new EditServices();
        private readonly IFormatService _formatService = new FormatService();
        private readonly IPrintService _printService = new PrintService();
        private readonly IPdfService _pdfService = new PdfService();
        private readonly IUnsavedChangesService _unsavedChangesService = new UnsavedChangesManager();
        private readonly IzoomService _zoomService = new ZoomService();
        private readonly IStatusBarService _statusBarService = new StatusBarService();
        private readonly IFeedbackService _feedbackService = new FeedbackService();
        private IFindService _findService;

        private int _zoomFactor = 100;
        private string _currentTextStatus = " Ready  ";

        private bool SaveCurrentFile() =>
            _fileService.Save(rtxtMainText.Text);

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            lblMessageStatus.Text = "Ready";
            statusBarToolStripMenuItem.Checked = statusBar.Visible;

            undoToolStripMenuItem.Enabled = false;
        }
        private void UpdateFormTitle()
        {
            if (!string.IsNullOrEmpty(_fileService.CurrentFilePath))
                this.Text = $"{Path.GetFileName(_fileService.CurrentFilePath)} - NoteLite";
            else
                this.Text = "Untitled - NoteLite";
        }
        private void UpdateStatusBar()
        {
            string text = rtxtMainText.Text;
            int caretIndex = rtxtMainText.SelectionStart;

            lblMessageStatus.Text = _statusBarService.GetTextStatus(text);
            lblLineColCount.Text = _statusBarService.GetLineAndColumn(text, caretIndex);
            lblDateAndTime.Text = _statusBarService.GetDateTimeStatus();
            lblZoom.Text = _statusBarService.GetZoomPercentage(_zoomFactor);
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = false;

            if (_unsavedChangesService.ConfirmSaveIfNeeded(SaveCurrentFile))
            {
                if (_fileService.OpenFile(rtxtMainText))
                {
                    _unsavedChangesService.Reset();
                    UpdateFormTitle();
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_fileService.Save(rtxtMainText.Text))
            {
                _unsavedChangesService.Reset();
                UpdateFormTitle();
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_fileService.SaveAs(rtxtMainText.Text))
            {
                _unsavedChangesService.Reset();
                UpdateFormTitle();
            }
        }

        private void menuEditCommand_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = rtxtMainText.CanUndo;
            if (sender is ToolStripMenuItem item && item.Tag is string command)
            {
                _editService.Execute(command, rtxtMainText);

            }
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formatService.BackColorPalette(colorDialog1, rtxtMainText);
        }
        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formatService.ForColorPalette(colorDialog1, rtxtMainText);
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formatService.ChangeFontProperties(fontDialog1, rtxtMainText);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _printService.Print(rtxtMainText.Text);
        }
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _printService.ShowPrintPreview(rtxtMainText.Text);
        }
        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(sender, e);
        }

        private void exportToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pdfService.SaveTextAsPdf(rtxtMainText.Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_unsavedChangesService.ConfirmSaveIfNeeded(SaveCurrentFile);
        }

        private void rtxtMainText_TextChanged(object sender, EventArgs e)
        {
            _unsavedChangesService.MarkModified();

            UpdateStatusBar();

            if (string.IsNullOrWhiteSpace(rtxtMainText.Text))
            {
                _currentTextStatus = "Empty text...  ";
            }
            else
            {
                _currentTextStatus = "Writing...  ";
                typingTimer.Stop(); // إعادة تشغيل المؤقت
                typingTimer.Start();
            }

            lblMessageStatus.Text = _currentTextStatus;

            undoToolStripMenuItem.Enabled = rtxtMainText.TextLength > 0 && rtxtMainText.CanUndo;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_unsavedChangesService.ConfirmSaveIfNeeded(SaveCurrentFile))
            {
                rtxtMainText.Clear();
                _fileService.ResetPath();
                _unsavedChangesService.Reset();
                UpdateFormTitle();
            }

        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoomService.ZoomIn(rtxtMainText);
            if (_zoomFactor < 500)
            {
                _zoomFactor += 10;
                rtxtMainText.ZoomFactor = _zoomFactor / 100f;
                UpdateStatusBar();
            }

        }
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoomService.ZoomOut(rtxtMainText);
            if (_zoomFactor > 10)
            {
                _zoomFactor -= 10;
                rtxtMainText.ZoomFactor = _zoomFactor / 100f;
                UpdateStatusBar();
            }
        }
        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zoomService.ResetZoom(rtxtMainText);
        }

        private void rtxtMainText_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateAndTime.Text = _statusBarService.GetDateTimeStatus();
        }
        private void typingTimer_Tick(object sender, EventArgs e)
        {
            typingTimer.Stop();
            _currentTextStatus = " Ready  ";
            lblMessageStatus.Text = _currentTextStatus;
        }
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBar.Visible = !statusBar.Visible;

            statusBarToolStripMenuItem.Checked = statusBar.Visible;
        }
        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _feedbackService.SendFeedback();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Check the press Ctrl + F
            if (e.Control && e.KeyCode == Keys.F)
            {

                if (_findService == null)
                    _findService = new FindService(rtxtMainText);

                frmFind findForm = new frmFind(_findService);
                findForm.Show(this);
            }
        }
        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This item has not yet been implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This item has not yet been implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutNoteLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }
        private void rightToLeftReadingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtMainText.RightToLeft = (rtxtMainText.RightToLeft == RightToLeft.Yes)
                ? RightToLeft.No : RightToLeft.Yes;

        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_findService == null)
                _findService = new FindService(rtxtMainText);

            var findForm = new frmFind(_findService);
            findForm.Show(this);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
