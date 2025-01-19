using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class UsersEditorWindow : Window
    {
        private int _idRole;
        public int YourId;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public UsersEditorWindow()
        {
            InitializeComponent();
        }

        public void DataLoad()
        {
            UsersGrid.ItemsSource = dataBase.Users.ToList();
        }

        private void UpdateUserRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = UsersGrid.SelectedItem as Users;
                update.idRole = _idRole;
                dataBase.Users.AddOrUpdate(update);
                dataBase.SaveChanges();
                DataLoad();

                if(update.idUser != YourId)
                {
                    Notifications notification = new Notifications()
                    {
                        idUser = update.idUser,
                        ContentNotification = $"Ваша роль изменилась. Вы теперь {update.Roles.NameRole}"
                    };
                    dataBase.Notifications.Add(notification);
                    dataBase.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось обновить роль.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
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

        private void UsersGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }

        private void UsersGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var select = UsersGrid.SelectedItem as Users;
                RolesBox.Text = select.Roles.NameRole;
                _idRole = select.idRole;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }
    }
}
