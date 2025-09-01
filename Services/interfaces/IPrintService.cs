using System;
using System.Drawing.Printing;


namespace NoteLite.Services
{
    public interface IPrintService
    {
        void Print(string text);
        void ShowPrintPreview(string text);
        void PrintPageHandler(object sender, PrintPageEventArgs e);
    }
}
