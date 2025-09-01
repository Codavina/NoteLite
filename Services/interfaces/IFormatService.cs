using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public interface IFormatService
    {
        void BackColorPalette(ColorDialog color, Control TextControl);
        void ForColorPalette(ColorDialog color, Control TextControl);
        void ChangeFontProperties(FontDialog fontDialog, Control TextControl);
    }
}
