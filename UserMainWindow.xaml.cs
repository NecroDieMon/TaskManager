using System.Data.Entity;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class UserMainWindow : Window
    {
        public int IDUser;

        public UserMainWindow()
        {
            InitializeComponent();
            OpenUsersWindow.Visibility = Visibility.Hidden;
        }

        private void OpenTaskWindow_Click(object sender, RoutedEventArgs e)
        {
            TaskEditorWindow taskEditorWindow = new TaskEditorWindow();
            taskEditorWindow.IDUserForTask = IDUser;
            taskEditorWindow.ShowDialog();
        }

        private void OpenProjectWindow_Click(object sender, RoutedEventArgs e)
        {
            ProjectEditorWindow projectEditorWindow = new ProjectEditorWindow();
            projectEditorWindow.IDUserForProject = IDUser;
            projectEditorWindow.ShowDialog();
        }

        private void OpenTeamWindow_Click(object sender, RoutedEventArgs e)
        {
            TeamsEditorWindow teamsEditorWindow = new TeamsEditorWindow();
            teamsEditorWindow.IDUserForTeam = IDUser;
            teamsEditorWindow.ShowDialog();
        }

        private void OpenPersonalWindow_Click(object sender, RoutedEventArgs e)
        {
            PersonalWindow personalWindow = new PersonalWindow();
            personalWindow.IDUserForLabel = IDUser;
            personalWindow.ShowDialog();
        }

        private void OpenUsersWindow_Click(object sender, RoutedEventArgs e)
        {
            UsersEditorWindow usersEditorWindow = new UsersEditorWindow();
            usersEditorWindow.YourId = IDUser;
            usersEditorWindow.ShowDialog();
        }

        private void ExitYourAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Notifications_Loaded(object sender, RoutedEventArgs e)
        {
            TaskManagerDBEntities dataBase = new TaskManagerDBEntities();
            var notificationsList = dataBase.Notifications.Where(p => p.idUser == IDUser).ToList();
            Notifications.ItemsSource = notificationsList;
        }
    }
}