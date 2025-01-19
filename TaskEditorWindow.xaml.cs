using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using TaskManager.DataBase;

namespace TaskManager
{
    public partial class TaskEditorWindow : Window
    {
        public int IDUserForTask;
        private int _idStatus;
        private int _indexPriority;
        private string _namePriority;
        private int _idProject;

        TaskManagerDBEntities dataBase = new TaskManagerDBEntities();

        public TaskEditorWindow()
        {
            InitializeComponent();
            ComboDataLoad();
        }

        public void DataLoad()
        {
            YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.idUser == IDUserForTask).ToList();
        }

        public void ComboDataLoad()
        {
            var statusList = dataBase.Status.ToList();
            StatusBox.ItemsSource = statusList;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd.MM.yyyy");

            try
            {
                Tasks task = new Tasks()
                {
                    idUser = IDUserForTask,
                    idProject = _idProject,
                    NameTask = NameTask.Text,
                    DescriptionTask = DescriptionTask.Text,
                    Priority = _namePriority,
                    DateAdd = formattedDate,
                    Comment = CommentTask.Text,
                    Deadline = CompletionDateTask.Text,
                    idStatus = _idStatus
                };
                dataBase.Tasks.Add(task);
                dataBase.SaveChanges();
                DataLoad();
                MessageBox.Show("Задача успешно создана.");
            }
            catch
            {
                MessageBox.Show("Не удалось создать задачу.");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delete = YourTasksGrid.SelectedItem as Tasks;
                dataBase.Tasks.Remove(delete);
                dataBase.SaveChanges();
                DataLoad();
                MessageBox.Show("Задача успешно удалена.");
            }
            catch
            {
                MessageBox.Show("Не удалось удалить задачу.");
            }
        }

        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var update = YourTasksGrid.SelectedItem as Tasks;
                update.idProject = _idProject;
                update.NameTask = NameTask.Text;
                update.DescriptionTask = DescriptionTask.Text;
                update.Priority = _namePriority;
                update.Comment = CommentTask.Text;
                update.Deadline = CompletionDateTask.Text;
                update.idStatus = _idStatus;
                dataBase.Tasks.AddOrUpdate(update);
                dataBase.SaveChanges();
                DataLoad();
            }
            catch
            {
                MessageBox.Show("Не удалось обновить задачу.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.NameTask == SearchTask.Text).ToList();
        }

        private void PriorityBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _indexPriority = PriorityBox.SelectedIndex;

            if(_indexPriority == 0)
            {
                _namePriority = "Легко";
            }
            else if(_indexPriority == 1)
            {
                _namePriority = "Средняя сложность";
            }
            else if(_indexPriority == 2)
            {
                _namePriority = "Сложно";
            }
        }

        private void StatusBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedStatus = StatusBox.SelectedItem as Status;

            if (selectedStatus != null)
            {
                _idStatus = selectedStatus.idStatus;
            }
        }

        private void ProjectsBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            var selectedProject = ProjectsBox.SelectedItem as Projects;

            if (selectedProject != null)
            {
                _idProject = selectedProject.idProject;
            }
        }

        private void SortBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int indexSort = SortBox.SelectedIndex;

            if(indexSort == 0)
            {
                YourTasksGrid.ItemsSource = dataBase.Tasks.ToList();
            }
            else if (indexSort == 1)
            {
                YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.Deadline == CompletionDateTask.Text).ToList();
            }
            else if (indexSort == 2)
            {
                YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.Priority == PriorityBox.Text).ToList();
            }
            else if (indexSort == 3)
            {
                YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.Status.NameStatus == StatusBox.Text).ToList();
            }
            else if (indexSort == 4)
            {
                YourTasksGrid.ItemsSource = dataBase.Tasks.Where(p => p.Projects.NameProject == ProjectsBox.Text).ToList();
            }
        }

        private void YourTasksGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var select = YourTasksGrid.SelectedItem as Tasks;

                if(select != null)
                {
                    ProjectsBox.Text = select.Projects.NameProject;
                    _idProject = select.idProject;
                    NameTask.Text = select.NameTask;
                    DescriptionTask.Text = select.DescriptionTask;
                    _namePriority = select.Priority;
                    PriorityBox.Text = select.Priority;
                    CommentTask.Text = select.Comment;
                    CompletionDateTask.Text = select.Deadline;
                    StatusBox.Text = select.Status.NameStatus;
                    _idStatus = select.idStatus;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }

        private void ProjectsBox_Loaded(object sender, RoutedEventArgs e)
        {
            var projectsList = dataBase.Projects.Where(p => p.idUser == IDUserForTask).ToList();
            ProjectsBox.ItemsSource = projectsList;
        }

        private void YourTasksGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }
    }
}
