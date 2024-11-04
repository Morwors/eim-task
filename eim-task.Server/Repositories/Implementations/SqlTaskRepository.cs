using eim_task.Server.Data;
using eim_task.Server.Models;
using eim_task.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eim_task.Server.Repositories.Implementations
{
    public class SqlTaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JSTask>> GetAllJSTasksAsync(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return await _context.JSTasks.Where(t => t.Title.Contains(filter)).ToListAsync();
            }
            return await _context.JSTasks.ToListAsync();
        }

        public async Task<JSTask> GetJSTaskByIdAsync(int id)
        {
            return await _context.JSTasks.FindAsync(id);
        }

        public async Task<JSTask> AddJSTaskAsync(JSTask task)
        {
            task.CreatedAt = DateTime.UtcNow;
            await _context.JSTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task; // Return the created task
        }

        public async Task<bool> UpdateJSTaskAsync(JSTask task)
        {
            var existingTask = await _context.JSTasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                return false; // Task not found
            }

            // Update properties
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            // Any additional properties can be updated here

            _context.JSTasks.Update(existingTask);
            await _context.SaveChangesAsync();
            return true; // Return true if update succeeds
        }

        public async Task<bool> DeleteJSTaskAsync(int id)
        {
            var task = await _context.JSTasks.FindAsync(id);
            if (task == null)
            {
                return false; // Task not found
            }

            _context.JSTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true; // Return true if deletion succeeds
        }
    }
}
