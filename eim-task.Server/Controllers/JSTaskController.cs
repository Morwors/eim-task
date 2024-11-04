using eim_task.Server.Models;
using eim_task.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eim_task.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IJSTaskService _taskService;

        public TasksController(IJSTaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JSTask>>> GetAllTasks(string filter = "")
        {
            var tasks = await _taskService.GetAllJSTasksAsync(filter);
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JSTask>> GetTaskById(int id)
        {
            var task = await _taskService.GetJSTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<JSTask>> CreateTask(JSTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taskService.AddJSTaskAsync(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, JSTask task)
        {
            var existingTask = await _taskService.GetJSTaskByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            await _taskService.UpdateJSTaskAsync(task);

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetJSTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            await _taskService.DeleteJSTaskAsync(id);

            return NoContent();
        }
    }
}
