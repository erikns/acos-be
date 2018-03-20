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
        private Repository repository;

        public TaskServiceImpl(Repository repository)
        {
            this.repository = repository;
        }
        
        public TaskModel Create(TaskModel task)
        {
            var forUserResult = repository.Users.Where(u => u.Email == task.UserEmail);
            if (forUserResult.Count() != 1) return null;
            var newTask = repository.Add(new Task
            {
                Title = task.Title,
                Description = task.Description,
                User = forUserResult.Single()
            });

            repository.SaveAll();
            return MapTaskToModel(newTask);
        }

        public bool Delete(int id)
        {
            var taskResult = repository.Tasks.Where(t => t.Id == id);
            if (taskResult.Count() == 1)
            {
                var task = taskResult.Single();
                repository.Remove(task);
                repository.SaveAll();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            var task = repository.Tasks.Include(t => t.User).Where(t => t.Id == id).Count();
            return task > 0;
        }

        public TaskModel Find(int id)
        {
            var result = repository.Tasks.Include(t => t.User).Where(t => t.Id == id);
            if (result.Count() == 1)
            {
                var task = result.Single(); 
                return MapTaskToModel(task);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskModel> FindAll()
        {
            return repository.Tasks.Include(t => t.User).Select(MapTaskToModel);
        }

        public IEnumerable<TaskModel> FindByUserEmail(string userEmail)
        {
            var result = repository.Tasks.Include(t => t.User).Where(t => t.User.Email == userEmail);
            return result.Select(MapTaskToModel);
        }

        public TaskModel Update(int id, TaskModel task)
        {
            var currentTask = repository.Tasks.Include(t => t.User).Where(t => t.Id == id).Single();
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;

            repository.Update(currentTask);
            repository.SaveAll();
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
