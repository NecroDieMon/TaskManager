using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow userMainWindow = new UserMainWindow();
            TeamsEditorWindow teamsEditorWindow = new TeamsEditorWindow();

            using (TaskManagerDBEntities dataBase = new TaskManagerDBEntities())
            {
                var query = dataBase.Users.Where(p => p.Login == YourLogin.Text && p.Password == YourPassword.Password).ToList();


                if (query != null)
                {
                    Close();
                    if (query[0].idRole == 1)
                    {
                        userMainWindow.OpenUsersWindow.Visibility = Visibility.Visible;
                    }
                    userMainWindow.IDUser = query[0].idUser;
                    userMainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Не удалось войти в акаунт.");
                }
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }

        private void RecoverThePassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow passwordRecoveryWindow = new PasswordRecoveryWindow();
            passwordRecoveryWindow.Show();
            Close();
        }
    }
}
