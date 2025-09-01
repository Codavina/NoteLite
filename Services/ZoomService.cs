using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class ZoomService : IzoomService
    {
        public void ZoomIn(RichTextBox textBox)
        {
            if (textBox.ZoomFactor < 5.0f) // maximum
            {
                textBox.ZoomFactor += 0.1f;
            }
        }
        public void ZoomOut(RichTextBox textBox)
        {
            if (textBox.ZoomFactor > 0.5f) // minimum
            {
                textBox.ZoomFactor -= 0.1f;
            }
        }
        public void ResetZoom(RichTextBox textBox)
        {
            textBox.ZoomFactor = 1.0f;
        }
    }
}
