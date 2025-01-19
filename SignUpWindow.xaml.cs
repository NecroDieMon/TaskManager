using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class SignUpWindow : Window
    {
        private int _idRole;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users users = new Users()
                {
                    idRole = _idRole,
                    Login = YourLogin.Text,
                    Password = YourPassword.Password,
                    Email = YourEmail.Text
                };
                dataBase.Users.Add(users);
                dataBase.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void RolesBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedRole = RolesBox.SelectedItem as Roles;

            if (selectedRole != null)
            {
                _idRole = selectedRole.idRole;
            }
        }

        private void RolesBox_Loaded(object sender, RoutedEventArgs e)
        {
            var rolesList = dataBase.Roles.ToList();
            RolesBox.ItemsSource = rolesList;
        }
    }
}