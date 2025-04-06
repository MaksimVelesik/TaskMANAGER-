using System.Windows;

namespace TaskManager
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (RegisterUser(username, password))
            {
                MessageBox.Show("Регистрация успешна! Теперь вы можете войти.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Попробуйте снова.");
            }
        }

        private bool RegisterUser(string username, string password)
        {
            return true;
        }
    }
}