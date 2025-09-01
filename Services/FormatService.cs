using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class FormatService : IFormatService
    {
        public void BackColorPalette(ColorDialog color, Control TextControl) 
        {
            if (color.ShowDialog() == DialogResult.OK)
            {

                TextControl.BackColor = color.Color;
            }
        }
        public void ForColorPalette(ColorDialog color, Control TextControl) 
        {
            if (color.ShowDialog() == DialogResult.OK)
            {

                TextControl.ForeColor = color.Color;
            }
        }
        public void ChangeFontProperties(FontDialog fontDialog, Control TextControl)
        {
            fontDialog.Font = TextControl.Font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                TextControl.Font = fontDialog.Font;
            }
        }
    }
}
