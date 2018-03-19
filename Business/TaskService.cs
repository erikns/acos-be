using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
using ACOS_be.Entities;
using ACOS_be.Models;

namespace ACOS_be.Business
{
    public interface TaskService
    {
        TaskModel Create(TaskModel task);
        TaskModel Find(int id);
        IEnumerable<TaskModel> FindAll();
        TaskModel Update(int id, TaskModel updatedTask);
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
        
        public TaskModel Create(TaskModel task)
        {
            var forUser = new User { Id = 42 };
            var newTask = context.Add(new Task
            {
                Title = task.Title,
                Description = task.Description,
                //User = forUser
            });

            context.SaveChanges();
            return MapTaskToModel(newTask.Entity);
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

        public TaskModel Find(int id)
        {
            var task = context.Tasks.Find(id);
            if (task != null)
            {
                return MapTaskToModel(task);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskModel> FindAll()
        {
            return context.Tasks.Select(MapTaskToModel);
        }

        public TaskModel Update(int id, TaskModel task)
        {
            var currentTask = context.Tasks.Find(id);
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;

            context.Update(currentTask);
            context.SaveChanges();
            return MapTaskToModel(currentTask);
        }

        private TaskModel MapTaskToModel(Task task)
        {
            return new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                //UserId = task.User.Id
            };
        }
    }
}
