using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public interface IzoomService
    {
        void ZoomIn(RichTextBox textBox);
        void ZoomOut(RichTextBox textBox);
        void ResetZoom(RichTextBox textBox);
    }
}
