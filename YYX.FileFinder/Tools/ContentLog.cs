using System;
using System.Windows.Forms;

namespace YYX.FileFinder.Tools
{
    public static class ContentLog
    {
        private static RichTextBox CurrentRichTextBox;
        private const int MaxCount = 1000;

        public static void Initialize(RichTextBox richTextBox)
        {
            CurrentRichTextBox = richTextBox;
        }

        public static void WriteLine(string text)
        {
            CurrentRichTextBox.BeginInvoke(new Action(() =>
            {
                if (CurrentRichTextBox != null)
                {
                    var linesLength = CurrentRichTextBox.Lines.Length;
                    var overflow = linesLength > MaxCount;
                    if (overflow)
                    {
                        CurrentRichTextBox.Clear();
                    }

                    CurrentRichTextBox.AppendText(text);
                    CurrentRichTextBox.AppendText(Environment.NewLine);

                    CurrentRichTextBox.SelectionStart = CurrentRichTextBox.Text.Length;
                    CurrentRichTextBox.ScrollToCaret();
                }
            }));
        }
    }
}
