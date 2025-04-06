using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Input;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<MyTask> AllTasks { get; } = new ObservableCollection<MyTask>();

        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeTasks();

            AddTaskCommand = new RelayCommand(async () => await AddTask());
            EditTaskCommand = new RelayCommand(async () => await EditTask(), CanEditOrDelete);
            DeleteTaskCommand = new RelayCommand(async () => await DeleteTask(), CanEditOrDelete);
            ExitCommand = new RelayCommand(ExitApplication);
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
            LoginCommand = new RelayCommand(OpenLoginWindow);

            DataContext = this;

            taskListView.SelectionChanged += (s, e) => UpdateCommandStates();
        }

        private void OpenRegisterWindow()
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void OpenLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void InitializeTasks()
        {
            AllTasks.Add(new MyTask { Title = "Задача 1", Description = "Описание 1", Priority = "Высокий", Status = "В работе" });
            AllTasks.Add(new MyTask { Title = "Задача 2", Description = "Описание 2", Priority = "Низкий", Status = "Выполнено" });
            taskListView.ItemsSource = AllTasks;
        }

        private async Task AddTask()
        {
            IsLoading = true;
            try
            {
                TaskDialog taskDialog = new TaskDialog();
                if (taskDialog.ShowDialog() == true)
                {
                    await Task.Delay(100);
                    AllTasks.Add(taskDialog.NewTask);
                    UpdateCommandStates();
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task EditTask()
        {
            IsLoading = true;
            try
            {
                if (taskListView.SelectedItem is MyTask selectedTask)
                {
                    TaskDialog taskDialog = new TaskDialog(selectedTask);
                    if (taskDialog.ShowDialog() == true)
                    {
                        await Task.Delay(100);
                        var index = AllTasks.IndexOf(selectedTask);
                        AllTasks[index] = taskDialog.NewTask;
                        UpdateCommandStates();
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteTask()
        {
            IsLoading = true;
            try
            {
                if (taskListView.SelectedItem is MyTask selectedTask)
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить эту задачу?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        await Task.Delay(100);
                        AllTasks.Remove(selectedTask);
                        UpdateCommandStates();
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool CanEditOrDelete()
        {
            return taskListView.SelectedItem != null;
        }

        private void UpdateCommandStates()
        {
            ((RelayCommand)EditTaskCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteTaskCommand).RaiseCanExecuteChanged();
        }

        private void FilterTasks(string status)
        {
            var filteredTasks = new ObservableCollection<MyTask>(AllTasks.Where(t => t.Status == status));
            taskListView.ItemsSource = filteredTasks;
        }

        private void FilterAll_Click(object sender, RoutedEventArgs e)
        {
            taskListView.ItemsSource = AllTasks;
        }

        private void FilterInProgress_Click(object sender, RoutedEventArgs e)
        {
            FilterTasks("В работе");
        }

        private void FilterCompleted_Click(object sender, RoutedEventArgs e)
        {
            FilterTasks("Выполнено");
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                Password = passwordBox.Password;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class MyTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}