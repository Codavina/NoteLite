using System;
using System.Text.RegularExpressions;


namespace NoteLite.Services
{
    public class StatusBarService : IStatusBarService
    {
        public string GetTextStatus(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "Empty text...";

            return "Writing...";
        }
        public string GetLineAndColumn(string text, int caretIndex)
        {
            int line = 1;
            int col = 1;

            for (int i = 0; i < caretIndex; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    col = 1;
                }
                else
                {
                    col++;
                }
            }

            return $" Ln {line}, Col {col} ";
        }
        public string GetDateTimeStatus()
        {
            return DateTime.Now.ToString(" yyyy-MM-dd hh:mm:ss tt ");
        }
        public string GetZoomPercentage(int zoomFactor)
        {
            return $" {zoomFactor}% ";
        }
        public int CountWords(string text)
        {
            return Regex.Matches(text, @" \b\w+\b ").Count;
        }
    }
}
