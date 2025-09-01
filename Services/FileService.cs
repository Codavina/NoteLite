using System;
using System.IO;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public class FileService:IFileService
    {
        public string CurrentFilePath { get; private set; } = string.Empty;
   
        public bool OpenFile(RichTextBox textBox)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Open File"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return false;

            try
            {
                textBox.Text = File.ReadAllText(ofd.FileName);
                CurrentFilePath = ofd.FileName;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file:\n{ex.Message}", "File Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Save(string content)
        {
            if (string.IsNullOrEmpty(CurrentFilePath))
                return SaveAs(content);

            try
            {
                File.WriteAllText(CurrentFilePath, content);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file:\n{ex.Message}", "File Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SaveAs(string content)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Save As",
                DefaultExt = "txt"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return false;

            try
            {
                File.WriteAllText(sfd.FileName, content);
                CurrentFilePath = sfd.FileName;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file:\n{ex.Message}", "File Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void ResetPath()
        {
            CurrentFilePath = string.Empty;
        }
    }
}

