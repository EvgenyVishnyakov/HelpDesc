using HelpDesk.Common;
using HelpDesk.Common.Models;
using System.Windows.Forms;

namespace HelpDeskWinFormsApp
{
    public partial class RegistrationForm : Form
    {
        private readonly IProvider provider;
        private string email;
        public RegistrationForm(IProvider provider)
        {
            InitializeComponent();

            this.provider = provider;
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!InspectEmail(emailTextBox.Text))
            {
                MessageBox.Show($"Вы вводите некорректный адрес. Давайте попробуем еще раз! Спасибо!");
                nameTextBox.Text = string.Empty;
                loginTextBox.Text = string.Empty;
                email = string.Empty;

                GetUserParameters();
            }
            else
            {

                if (DialogResult == DialogResult.Cancel || DialogResult == DialogResult.Abort)
                {
                    return;
                }
                User user = GetUserParameters();
                email = emailTextBox.Text;
                provider.AddUser(user);
            }
        }
        private User GetUserParameters()
        {
            return new User
            {
                Name = nameTextBox.Text,
                Login = loginTextBox.Text,
                Password = Methods.GetHashMD5(passwordTextBox.Text),
                Email = email
            };
        }

        public bool InspectEmail(string Text)
        {
            var emailLenghth = emailTextBox.Text.ToString().Length;
            var controlEmailLenghth = 0;
            for (int i = 0; i < emailLenghth; i++)
            {
                if (emailTextBox.Text[i] == '@' || emailTextBox.Text[i] == '.' && emailLenghth > 6)
                {
                    controlEmailLenghth++;
                }
            }
            if (controlEmailLenghth == 2)
            {
                return true;
            }

            return false;
        }


    }
}
