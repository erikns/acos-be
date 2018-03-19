using System.Collections.Generic;
using System.Linq;
using ACOS_be.Messages;
using ACOS_be.Models;
//using ACOS_be.Models;

namespace ACOS_be.Business
{
    public interface TaskService
    {
        TaskMessage Create(TaskMessage task);
        TaskMessage Find(int id);
        IEnumerable<TaskMessage> FindAll();
        TaskMessage Update(int id, TaskMessage updatedTask);
        bool Delete(int id);
        bool Exists(int id);
    }

    public class TaskServiceImpl : TaskService
    {
        private static List<Task> tasks = new List<Task>();
        private static int nextId = 0;
        
        public TaskMessage Create(TaskMessage task)
        {
            var forUser = new User { Id = 42 };
            var newTask = new Task
            {
                Id = ++nextId,
                Title = task.Title,
                Description = task.Description,
                User = forUser
            };

            tasks.Add(newTask);
            return MapTaskToMessage(newTask);
        }

        public bool Delete(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            tasks.Remove(task);
            return true;
        }

        public bool Exists(int id)
        {
            return tasks.Find(t => t.Id == id) != null;
        }

        public TaskMessage Find(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                return MapTaskToMessage(task);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskMessage> FindAll()
        {
            return tasks.Select(MapTaskToMessage);
        }

        public TaskMessage Update(int id, TaskMessage task)
        {
            var currentTask = tasks.Find(t => t.Id == id);
            var updatedTask = new Task
            {
                Id = currentTask.Id,
                Title = task.Title,
                Description = task.Description,
                User = new User { Id = 42 }
            };

            tasks.Remove(currentTask);
            tasks.Add(updatedTask);
            return MapTaskToMessage(updatedTask);
        }

        private TaskMessage MapTaskToMessage(Task task)
        {
            return new TaskMessage
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                UserId = task.User.Id
            };
        }
    }
}
