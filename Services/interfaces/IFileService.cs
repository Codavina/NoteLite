using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public interface IFileService
    {
        string CurrentFilePath { get; }
        bool OpenFile(RichTextBox textBox);
        bool Save(string content);
        bool SaveAs(string content);
        void ResetPath();
    }
}
