using System.Collections.Generic;
using ACOS_be.Messages;
//using ACOS_be.Models;

namespace ACOS_be.Business
{
    public interface TaskService
    {
        TaskMessage Create(TaskMessage task);
        TaskMessage Find(int id);
        IEnumerable<TaskMessage> FindAll();
        TaskMessage Update(TaskMessage updatedTask);
        bool Delete(TaskMessage task);
        bool Exists(int id);
    }

    public class TaskServiceImpl : TaskService
    {
        public TaskMessage Create(TaskMessage task)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(TaskMessage task)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public TaskMessage Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TaskMessage> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public TaskMessage Update(TaskMessage updatedTask)
        {
            throw new System.NotImplementedException();
        }
    }
}
