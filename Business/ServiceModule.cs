using System;
using ACOS_be.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ACOS_be.Business
{
    public delegate void ModuleDelegate<T>(T e);

    public class TaskAddedEventArgs : EventArgs
    {
        public User User { get; set; }
        public Task Task { get; set; }
    }

    public class UserAddedEventArgs : EventArgs
    {
        public User User { get; set; }
    }

    public class ModuleEvents
    {
        public event ModuleDelegate<TaskAddedEventArgs> TaskAdded;
        public event ModuleDelegate<UserAddedEventArgs> UserAdded;

        public void OnTaskAdded(TaskAddedEventArgs e)
        {
            if (TaskAdded != null) TaskAdded(e);
        }

        public void OnUserAdded(UserAddedEventArgs e)
        {
            if (UserAdded != null) UserAdded(e);
        }
    }

    public interface EventsFactory
    {
        ModuleEvents GetEvents();
    }

    public class EventsFactoryImpl : EventsFactory
    {
        private static ModuleEvents events = new ModuleEvents();

        public ModuleEvents GetEvents()
        {
            return events;
        }
    }

    public interface ServiceModule
    {
        void Initialize(EventsFactory events);
    }

    public class ModuleRegistry
    {
        private EventsFactory events;

        public ModuleRegistry(EventsFactory events)
        {
            this.events = events;
        }

        public void Register<T>() where T : ServiceModule, new()
        {
            var module = new T();
            module.Initialize(events);
        }
    }
}