using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class PrintService:IPrintService
    {
        private string _textToPrint;
        private int _currentCharIndex;
        public void Print(string text)
        {
            _textToPrint = text;
            _currentCharIndex = 0;

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "NoteLite Document";
            printDoc.PrintPage += PrintPageHandler;

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDoc
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        public void ShowPrintPreview(string text)
        {
            _textToPrint = text;
            _currentCharIndex = 0;

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "NoteLite Document";
            printDoc.PrintPage += PrintPageHandler;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 800,
                Height = 600
            };

            previewDialog.ShowDialog();
        }

        public void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);
            int charsFitted, linesFilled;

            RectangleF printArea = e.MarginBounds;

            e.Graphics.MeasureString(
                _textToPrint.Substring(_currentCharIndex),
                font,
                printArea.Size,
                StringFormat.GenericDefault,
                out charsFitted,
                out linesFilled
            );

            e.Graphics.DrawString(
                _textToPrint.Substring(_currentCharIndex, charsFitted),
                font,
                Brushes.Black,
                printArea
            );

            _currentCharIndex += charsFitted;
            e.HasMorePages = (_currentCharIndex < _textToPrint.Length);
        }
    }
}
