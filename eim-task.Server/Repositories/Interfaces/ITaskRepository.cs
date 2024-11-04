using eim_task.Server.Models;

namespace eim_task.Server.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<JSTask>> GetAllJSTasksAsync(string filter);
        Task<JSTask> GetJSTaskByIdAsync(int id);
        Task<JSTask> AddJSTaskAsync(JSTask task);
        Task<bool> UpdateJSTaskAsync(JSTask task);
        Task<bool> DeleteJSTaskAsync(int id);
    }
}