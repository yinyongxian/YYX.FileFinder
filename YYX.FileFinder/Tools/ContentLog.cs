﻿using System;
using System.Windows.Forms;

namespace YYX.FileFinder.Tools
{
    public static class ContentLog
    {
        private static RichTextBox currentRichTextBox;
        private const int MaxCount = 1000;

        public static void Initialize(RichTextBox richTextBox)
        {
            currentRichTextBox = richTextBox;
        }

        public static void WriteLine(string text)
        {
            currentRichTextBox.BeginInvoke(new Action(() =>
            {
                if (currentRichTextBox != null)
                {
                    var linesLength = currentRichTextBox.Lines.Length;
                    var overflow = linesLength > MaxCount;
                    if (overflow)
                    {
                        currentRichTextBox.Clear();
                    }

                    currentRichTextBox.AppendText(text);
                    currentRichTextBox.AppendText(Environment.NewLine);

                    currentRichTextBox.SelectionStart = currentRichTextBox.Text.Length;
                    currentRichTextBox.ScrollToCaret();
                }
            }));
        }
    }
}
