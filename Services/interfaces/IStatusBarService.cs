using System;
using System.Text.RegularExpressions;


namespace NoteLite.Services
{
    public interface IStatusBarService
    {
        string GetTextStatus(string text);
        string GetLineAndColumn(string text, int caretIndex);
        string GetDateTimeStatus();
        string GetZoomPercentage(int zoomFactor);
        int CountWords(string text);
    }
}
