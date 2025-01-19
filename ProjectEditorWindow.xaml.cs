using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class ProjectEditorWindow : Window
    {
        public int IDUserForProject;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public ProjectEditorWindow()
        {
            InitializeComponent();
        }

        public void DataLoad()
        {
            YourProjectsGrid.ItemsSource = dataBase.Projects.Where(p => p.idUser == IDUserForProject).ToList();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects project = new Projects()
                {
                    idUser = IDUserForProject,
                    NameProject = NameProject.Text,
                    DescriptionProject = DescriptionProject.Text,
                };
                dataBase.Projects.Add(project);
                dataBase.SaveChanges();
                DataLoad();
                MessageBox.Show("Проект успешно добавлен.");
            }
            catch
            {
                MessageBox.Show("Не удалось добавить проект.");
            }
        }

        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = YourProjectsGrid.SelectedItem as Projects;
                dataBase.Projects.Remove(delete);
                dataBase.SaveChanges();
                DataLoad();
                MessageBox.Show("Проект успешно удалён.");
            }
            catch
            {
                MessageBox.Show("Не удалось удалить проект.");
            }
        }

        private void UpdateProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = YourProjectsGrid.SelectedItem as Projects;
                update.NameProject = NameProject.Text;
                update.DescriptionProject = DescriptionProject.Text;
                dataBase.Projects.AddOrUpdate(update);
                dataBase.SaveChanges();
                DataLoad();
            }
            catch
            {
                MessageBox.Show("Не удалось обновить проект.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void YourProjectsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }

        private void YourProjectsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var select = YourProjectsGrid.SelectedItem as Projects;

                if (select != null)
                {
                    NameProject.Text = select.NameProject;
                    DescriptionProject.Text = select.DescriptionProject;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }
    }
}