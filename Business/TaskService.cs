using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
using ACOS_be.Entities;
using ACOS_be.Models;
using Microsoft.EntityFrameworkCore;

namespace ACOS_be.Business
{
    public interface TaskService : Service<TaskModel>
    {
        IEnumerable<TaskModel> FindByUserEmail(string userEmail);
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
            var forUser = context.Users.Single(u => u.Email == task.UserEmail);
            var newTask = context.Add(new Task
            {
                Title = task.Title,
                Description = task.Description,
                User = forUser
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
            var task = context.Tasks.Include(t => t.User).Where(t => t.Id == id).Count();
            return task > 0;
        }

        public TaskModel Find(int id)
        {
            var task = context.Tasks.Include(t => t.User).Where(t => t.Id == id).Single();
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
            return context.Tasks.Include(t => t.User).Select(MapTaskToModel);
        }

        public IEnumerable<TaskModel> FindByUserEmail(string userEmail)
        {
            var result = context.Tasks.Include(t => t.User).Where(t => t.User.Email == userEmail);
            return result.Select(MapTaskToModel);
        }

        public TaskModel Update(int id, TaskModel task)
        {
            var currentTask = context.Tasks.Include(t => t.User).Where(t => t.Id == id).Single();
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
                UserEmail = task.User.Email
            };
        }
    }
}
