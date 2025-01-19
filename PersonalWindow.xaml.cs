using System.Windows;
using System.Linq;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class PersonalWindow : Window
    {
        public int IDUserForLabel;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public PersonalWindow()
        {
            InitializeComponent();
        }

        private void OpenPasswordRecoveryWindow_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow passwordRecoveryWindow = new PasswordRecoveryWindow();
            passwordRecoveryWindow.Back.Visibility = Visibility.Hidden;
            passwordRecoveryWindow.WindowState = WindowState.Minimized;
            passwordRecoveryWindow.ShowDialog();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = dataBase.Users.Where(p => p.idUser == IDUserForLabel).ToList();

            YourLogin.Text = query[0].Login;
            YourEmail.Text = query[0].Email;
        }

        private void YourTasksGrid_Loaded(object sender, RoutedEventArgs e)
        {
            YourTasksGrid.ItemsSource = dataBase.Teams.Where(p => p.idUser == IDUserForLabel).ToList();
        }
    }
}
