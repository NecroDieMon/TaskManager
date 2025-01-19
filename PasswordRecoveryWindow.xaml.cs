using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class PasswordRecoveryWindow : Window
    {
        public PasswordRecoveryWindow()
        {
            InitializeComponent();
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            using (TaskManagerDBEntities dataBase = new TaskManagerDBEntities())
            {
                var user = dataBase.Users.FirstOrDefault(p => p.Login == YourLogin.Text);

                if (user != null)
                {
                    user.Password = YourNewPassword.Password.ToString();

                    dataBase.SaveChanges();
                    MessageBox.Show("Вы успешно изменили пароль!");
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}