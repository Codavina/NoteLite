using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class PdfService:IPdfService
    {
        public void SaveTextAsPdf(string content)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Save As PDF",
                DefaultExt = "pdf"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                    GlobalFontSettings.UseWindowsFontsUnderWindows = true;

                    document.Info.Title = "NoteLite Document";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Arial", 10, XFontStyleEx.Bold);

                    const double margin = 40;
                    double y = margin;
                    double lineHeight = font.GetHeight();

                    StringReader reader = new StringReader(content);
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        gfx.DrawString(line, font, XBrushes.Black,
                            new XRect(margin, y, page.Width - 2 * margin, page.Height - 2 * margin),
                            XStringFormats.TopLeft);
                        y += lineHeight;

                        // Start a new page if needed
                        if (y + lineHeight > page.Height - margin)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            y = margin;
                        }
                    }

                    document.Save(sfd.FileName);
                    MessageBox.Show("PDF saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optional: open the file automatically
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save PDF:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
