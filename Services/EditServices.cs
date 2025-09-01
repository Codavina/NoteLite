using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public class EditServices:IEditService
    {
        public void Execute(string command, RichTextBox rtb)
        {
            if (rtb == null || string.IsNullOrEmpty(command))
                return;

            switch (command.ToLower())
            {
                case "copy":
                    rtb.Copy();
                    break;
                case "cut":
                    rtb.Cut();
                    break;
                case "paste":
                    rtb.Paste();
                    break;
                case "delete":
                    if (!string.IsNullOrEmpty(rtb.SelectedText))
                        rtb.SelectedText = string.Empty;
                    break;
                case "undo":
                    if (rtb.CanUndo)
                        rtb.Undo();
                    break;
                case "selectall":
                    rtb.SelectAll();
                    break;
                default:
                    MessageBox.Show($"Unknown command: {command}");
                    break;
            }
        }
    }
}
