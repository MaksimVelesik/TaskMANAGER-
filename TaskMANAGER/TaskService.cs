using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManegerr
{
    public class TaskService
    {
        private List<TaskModel> _tasks;
        private List<TaskCategoryModel> _categories;

        public TaskService()
        {
            _tasks = new List<TaskModel>();
            _categories = new List<TaskCategoryModel>
            {
                new TaskCategoryModel { Name = "Работа" },
                new TaskCategoryModel { Name = "Личное" },
                new TaskCategoryModel { Name = "Учеба" }
            };
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync()
        {
            await Task.Delay(2000);
            return _tasks;
        }

        public async Task<IEnumerable<TaskCategoryModel>> GetCategoriesAsync()
        {
            await Task.Delay(2000);
            return _categories;
        }

        public void AddTask(TaskModel task) => _tasks.Add(task);
        public void RemoveTask(TaskModel task) => _tasks.Remove(task);
    }
}