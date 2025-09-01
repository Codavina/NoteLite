using System;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public interface IEditService
    {
        void Execute(string command, RichTextBox textBox);
    }
}
