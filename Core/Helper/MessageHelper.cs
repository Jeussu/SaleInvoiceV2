using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Core.Helper
{
    public class MessageHelper
    {
        public static void ShowException(Exception ex, string caption = "Exception")
        {
            MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError(string message, string caption = "Error")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfomation(string message, string caption = "Infomation")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarning(string message, string caption = "Warning")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowQuestion(string message, string caption = "Question", MessageBoxButtons buttons = MessageBoxButtons.OKCancel)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
        }
    }
}
