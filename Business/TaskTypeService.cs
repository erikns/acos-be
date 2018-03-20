using System;
using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
using ACOS_be.Entities;
using ACOS_be.Models;

namespace ACOS_be.Business
{
    public interface TaskTypeService : Service<TaskTypeModel, int>
    {}

    public class TaskTypeServiceImpl : TaskTypeService
    {
        private Repository repository;

        public TaskTypeServiceImpl(Repository repository)
        {
            this.repository = repository;
        }

        public TaskTypeModel Create(TaskTypeModel entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public TaskTypeModel Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TaskTypeModel> FindAll()
        {
            return repository.TaskTypes.Select(MapTaskTypeToModel);
        }

        public TaskTypeModel Update(int id, TaskTypeModel updated)
        {
            throw new System.NotImplementedException();
        }

        private TaskTypeModel MapTaskTypeToModel(TaskType taskType)
        {
            return new TaskTypeModel
            {
                Id = taskType.Id,
                Name = taskType.Name
            };
        }
    }
}