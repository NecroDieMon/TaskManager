using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class TeamsEditorWindow : Window
    {
        public int IDUserForTeam;
        private int _idMember;
        private int _idTask;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public TeamsEditorWindow()
        {
            InitializeComponent();
        }

        public void DataLoad()
        {
            YourTeamsGrid.ItemsSource = dataBase.Teams.Where(p => p.idUser == IDUserForTeam).ToList();
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Teams team = new Teams()
                {
                    NameTeam = NameTeam.Text,
                    idUser = _idMember,
                    idTask = _idTask
                };
                dataBase.Teams.Add(team);
                dataBase.SaveChanges();
                DataLoad();
            }
            catch
            {
                MessageBox.Show("Не удалось добавить команду.");
            }
        }

        private void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = YourTeamsGrid.SelectedItem as Teams;
                dataBase.Teams.Remove(delete);
                dataBase.SaveChanges();
                DataLoad();
            }
            catch
            {
                MessageBox.Show("Не удалось удалить команду.");
            }
        }

        private void UpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = YourTeamsGrid.SelectedItem as Teams;
                update.NameTeam = NameTeam.Text;
                update.idUser = _idMember;
                update.idTask = _idTask;
                dataBase.Teams.AddOrUpdate(update);
                dataBase.SaveChanges();
                DataLoad();
            }
            catch
            {
                MessageBox.Show("Не удалось обновить команду.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UsersBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedMember = UsersBox.SelectedItem as Users;

            if (selectedMember != null)
            {
                _idMember = selectedMember.idUser;
            }
        }

        private void TasksBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedTask = TasksBox.SelectedItem as Tasks;

            if (selectedTask != null)
            {
                _idTask = selectedTask.idTask;
            }
        }

        private void YourTeamsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var select = YourTeamsGrid.SelectedItem as Teams;

                if(select != null)
                {
                    NameTeam.Text = select.NameTeam;
                    _idMember = select.idUser;
                    UsersBox.Text = select.Users.Login;
                    _idTask = select.idTask;
                    TasksBox.Text = select.Tasks.NameTask;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }

        private void UsersBox_Loaded(object sender, RoutedEventArgs e)
        {
            var usersList = dataBase.Users.ToList();
            UsersBox.ItemsSource = usersList;
        }

        private void TasksBox_Loaded(object sender, RoutedEventArgs e)
        {
            var tasksList = dataBase.Tasks.Where(p => p.idUser == IDUserForTeam).ToList();
            TasksBox.ItemsSource = tasksList;
        }

        private void YourTeamsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }
    }
}