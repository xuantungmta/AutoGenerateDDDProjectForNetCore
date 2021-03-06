namespace AutoGenerateDDDProjectForNetCore.controls
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        public static Task Info(this RichTextBox txtLog, string message) { return txtLog.Log("[INFO] " + message, Color.Green); }
        public static Task Warn(this RichTextBox txtLog, string message) { return txtLog.Log("[WARN] " + message, Color.YellowGreen); }
        public static Task Debug(this RichTextBox txtLog, string message) { return txtLog.Log("[DEBUG] " + message, Color.Red); }
        public static Task Log(this RichTextBox txtLog, string message) { return txtLog.Log(message, Color.Black); }
        public static Task Log(this RichTextBox txtLog, string message, Color color)
        {
            return Task.Factory.StartNew(() =>
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new MethodInvoker(() => { txtLog.AppendText(message + "\n", color); }));
                }
                else
                {
                    txtLog.AppendText(message + "\n", color);
                }
            });
        }
    }
}