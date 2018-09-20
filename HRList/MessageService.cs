using System.Windows.Forms;

namespace HRList
{



    public class MessageService: IMessageService
    {

       public  void ShowMessage(string MessageText)
        {
            MessageBox.Show(MessageText, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string ErrorText)
        {
            MessageBox.Show(ErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
