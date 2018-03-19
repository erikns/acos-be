using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACOS_be.Business;
using ACOS_be.Messages;
using Microsoft.AspNetCore.Mvc;

namespace ACOS_be.Controllers
{
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        private TaskService taskService;

        public TasksController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(taskService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            return Ok(taskService.Find(id));
        }

        [HttpPost]
        public IActionResult CreateNewTask([FromBody]TaskMessage task)
        {
            var createdTask = taskService.Create(task);
            return Ok(createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody]TaskMessage task)
        {
            if (taskService.Exists(id))
            {
                var updatedTask = taskService.Update(task);
                return Ok(updatedTask);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (taskService.Exists(id))
            {
                var task = taskService.Find(id);
                if (taskService.Delete(task))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
