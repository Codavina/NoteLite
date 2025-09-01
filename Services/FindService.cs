using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class FindService : IFindService
    {
        private readonly RichTextBox _textBox;
        private int _lastIndex = 0;

       
        public FindService(RichTextBox textBox)
        {
            _textBox = textBox;
        }
        public bool FindNext(string searchText, bool matchCase = false)
        {
            if (string.IsNullOrEmpty(searchText)) return false;

            StringComparison comparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int index = _textBox.Text.IndexOf(searchText, _lastIndex, comparison);

            if (index != -1)
            {
                _textBox.Select(index, searchText.Length);
                _textBox.ScrollToCaret();
                _textBox.Focus();

                _lastIndex = index + searchText.Length; // to find next

                return true;
            }
            else
            {
                _lastIndex = 0; // to reset and find from the begain
                return false;
            }
        }

        public void Reset()
        {
            _lastIndex = 0;
        }
    }
}
