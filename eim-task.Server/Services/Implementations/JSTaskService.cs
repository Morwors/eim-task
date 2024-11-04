using eim_task.Server.Models;
using eim_task.Server.Repositories.Interfaces;
using eim_task.Server.Services.Interfaces;


namespace YourProject.Services
{
    public class JSTaskService : IJSTaskService
    {
        private readonly ITaskRepository _taskRepository;

        public JSTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<JSTask>> GetAllJSTasksAsync(string filter)
        {
            return await _taskRepository.GetAllJSTasksAsync(filter);
        }

        public async Task<JSTask> GetJSTaskByIdAsync(int id)
        {
            return await _taskRepository.GetJSTaskByIdAsync(id);
        }

        public async Task<JSTask> AddJSTaskAsync(JSTask task)
        {
            // Business logic: Automatically set CreatedAt if not set
            if (task.CreatedAt == null)
            {
                task.CreatedAt = DateTime.UtcNow;
            }

            return await _taskRepository.AddJSTaskAsync(task);
        }

        public async Task<bool> UpdateJSTaskAsync(JSTask task)
        {
            // Additional business logic could go here (e.g., validation, additional checks)

            var existingTask = await _taskRepository.GetJSTaskByIdAsync(task.Id);
            if (existingTask == null)
            {
                return false; // Task not found
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            // You can also update other properties here if needed

            return await _taskRepository.UpdateJSTaskAsync(existingTask);
        }

        public async Task<bool> DeleteJSTaskAsync(int id)
        {
            var task = await _taskRepository.GetJSTaskByIdAsync(id);
            if (task == null)
            {
                return false; // Task not found
            }

            return await _taskRepository.DeleteJSTaskAsync(id);
        }
    }
}
