using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
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
        private ApplicationContext context;

        public TaskServiceImpl(ApplicationContext context)
        {
            this.context = context;
        }
        
        public TaskMessage Create(TaskMessage task)
        {
            var forUser = new User { Id = 42 };
            var newTask = context.Add(new Task
            {
                Title = task.Title,
                Description = task.Description,
                User = forUser
            });

            context.SaveChanges();
            return MapTaskToMessage(newTask.Entity);
        }

        public bool Delete(int id)
        {
            var task = context.Tasks.Find(id);
            if (task != null)
            {
                context.Remove(task);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            var task = context.Tasks.Find(id);
            return task != null;
        }

        public TaskMessage Find(int id)
        {
            var task = context.Tasks.Find(id);
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
            return context.Tasks.Select(MapTaskToMessage);
        }

        public TaskMessage Update(int id, TaskMessage task)
        {
            var currentTask = context.Tasks.Find(id);
            var updatedTask = new Task
            {
                Id = currentTask.Id,
                Title = task.Title,
                Description = task.Description,
                User = new User { Id = 42 }
            };

            context.Update(updatedTask);
            context.SaveChanges();
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
