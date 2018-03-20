using System;
using ACOS_be.Business;

namespace ACOS_be.Modules
{
    public class UserNotificationModule : ServiceModule
    {
        public void Initialize(EventsFactory events)
        {
            events.GetEvents().TaskAdded += OnTaskAdded;
            events.GetEvents().UserAdded += OnUserAdded;
        }

        private void OnTaskAdded(TaskAddedEventArgs e)
        {
            Console.WriteLine($"EMAIL TO {e.User.Email} => A task was added to you: {e.Task.Title}");
        }

        private void OnUserAdded(UserAddedEventArgs e)
        {
            Console.WriteLine($"EMAIL TO {e.User.Email} => You were added as a user to the task system");
        }
    }
}