using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
using ACOS_be.Entities;
using ACOS_be.Models;
using Microsoft.EntityFrameworkCore;

namespace ACOS_be.Business
{
    public interface TaskService : Service<TaskModel, int>
    {
        IEnumerable<TaskCleanModel> FindByUserEmail(string userEmail);
    }

    public class TaskServiceImpl : TaskService
    {
        private Repository repository;
        private EventsFactory events;

        public TaskServiceImpl(Repository repository, EventsFactory events)
        {
            this.repository = repository;
            this.events = events;
        }
        
        public TaskModel Create(TaskModel task)
        {
            var forUserResult = repository.Users.Where(u => u.Email == task.UserEmail);
            if (forUserResult.Count() != 1) return null;
            var forUser = forUserResult.Single();

            var forTypeResult = repository.TaskTypes.Where(u => u.Name == task.TypeName);
            if (forTypeResult.Count() != 1) return null;
            var forType = forTypeResult.Single();

            var newTask = repository.Add(new Task
            {
                Title = task.Title,
                Description = task.Description,
                User = forUser,
                Type = forType
            });

            repository.SaveAll();

            var args = new TaskAddedEventArgs
            {
                User = forUser,
                Task = newTask
            };
            events.GetEvents().OnTaskAdded(args);

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
            var task = repository.Tasks.Where(t => t.Id == id).Count();
            return task > 0;
        }

        public TaskModel Find(int id)
        {
            var result = repository.Tasks.Include(t => t.User).Include(t => t.Type).Where(t => t.Id == id);
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
            return repository.Tasks.Include(t => t.User).Include(t => t.Type).Select(MapTaskToModel);
        }

        public IEnumerable<TaskCleanModel> FindByUserEmail(string userEmail)
        {
            var result = repository.Tasks.Include(t => t.User).Include(t => t.Type).Where(t => t.User.Email == userEmail);
            return result.Select(MapTaskToCleanModel);
        }

        public TaskModel Update(int id, TaskModel task)
        {
            var currentTask = repository.Tasks.Include(t => t.User).Include(t => t.Type).Where(t => t.Id == id).Single();
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;

            if (currentTask.Type.Name != task.TypeName)
            {
                var newTypeResult = repository.TaskTypes.Where(t => t.Name == task.TypeName);
                if (newTypeResult.Count() == 1)
                {
                    var newType = newTypeResult.Single();
                    currentTask.Type = newType;
                }
            }

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
                UserEmail = task.User.Email,
                TypeName = task.Type.Name,
            };
        }

        private TaskCleanModel MapTaskToCleanModel(Task task)
        {
            return new TaskCleanModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TypeName = task.Type.Name,
            };
        }
    }
}
