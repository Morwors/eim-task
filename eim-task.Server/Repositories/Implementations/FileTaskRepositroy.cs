using eim_task.Server.Models;
using eim_task.Server.Repositories.Interfaces;
using System.Text.Json;

namespace eim_task.Server.Repositories.Implementations
{
    public class FileTaskRepository : ITaskRepository
    {
        private readonly string _filePath = "tasks.json";
        private static readonly object _fileLock = new object();

        public async Task<IEnumerable<JSTask>> GetAllJSTasksAsync(string filter)
        {
            lock (_fileLock)
            {
                if (!File.Exists(_filePath))
                    return new List<JSTask>();

                var jsonData = File.ReadAllText(_filePath);
                //find all JSTask where filter similar to title
                if (!string.IsNullOrEmpty(filter))
                {

                    var tasks = JsonSerializer.Deserialize<List<JSTask>>(jsonData);
                    return tasks.Where(t => t.Title.Contains(filter)).ToList();
                }
                    return JsonSerializer.Deserialize<List<JSTask>>(jsonData);
            }
        }

        public async Task<JSTask> GetJSTaskByIdAsync(int id)
        {
            var tasks = await GetAllJSTasksAsync("");
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<JSTask> AddJSTaskAsync(JSTask task)
        {
            var tasks = (await GetAllJSTasksAsync("")).ToList();
            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            task.CreatedAt = DateTime.UtcNow;
            tasks.Add(task);

            SaveToFile(tasks);
            return task;
        }

        public async Task<bool> UpdateJSTaskAsync(JSTask task)
        {
            var tasks = (await GetAllJSTasksAsync("")).ToList();
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                tasks.Remove(existingTask);
                tasks.Add(task);

                SaveToFile(tasks);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteJSTaskAsync(int id)
        {
            var tasks = (await GetAllJSTasksAsync("")).ToList();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveToFile(tasks);
                return true;
            }
            return false;
        }

        private void SaveToFile(IEnumerable<JSTask> tasks)
        {
            lock (_fileLock)
            {
                var jsonData = JsonSerializer.Serialize(tasks);
                File.WriteAllText(_filePath, jsonData);
            }
        }
    }
}
