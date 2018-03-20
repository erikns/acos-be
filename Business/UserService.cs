using System.Collections.Generic;
using System.Linq;
using ACOS_be.Data;
using ACOS_be.Entities;
using ACOS_be.Models;

namespace ACOS_be.Business
{
    public interface UserService : Service<UserModel, int>
    {
    }

    public class UserServiceImpl : UserService
    {
        private Repository repository;
        private EventsFactory events;

        public UserServiceImpl(Repository repository, EventsFactory events)
        {
            this.repository = repository;
            this.events = events;
        }

        public UserModel Create(UserModel entity)
        {
            var newUser = repository.Add(new User {
                Name = entity.FullName,
                Email = entity.Email
            });

            repository.SaveAll();

            events.GetEvents().OnUserAdded(new UserAddedEventArgs
            {
                User = newUser
            });

            return MapUserToModel(newUser);
        }

        public bool Delete(int id)
        {
            var userResult = repository.Users.Where(u => u.Id == id);
            if (userResult.Count() == 1)
            {
                repository.Remove(userResult.Single());
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
            return repository.Users.Where(u => u.Id == id).Count() == 1;
        }

        public UserModel Find(int id)
        {
            var userResult = repository.Users.Where(u => u.Id == id);
            if (userResult.Count() == 1)
            {
                return MapUserToModel(userResult.Single());
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UserModel> FindAll()
        {
            return repository.Users.Select(MapUserToModel);
        }

        public UserModel Update(int id, UserModel updated)
        {
            var userResult = repository.Users.Where(u => u.Id == id);
            if (userResult.Count() == 1)
            {
                var user = userResult.Single();
                user.Email = updated.Email;
                user.Name = updated.FullName;
                var updatedUser = repository.Update(user);
                repository.SaveAll();
                return MapUserToModel(updatedUser);
            }
            else
            {
                return null;
            }
        }

        private UserModel MapUserToModel(User user)
        {
            return new UserModel {
                Id = user.Id,
                FullName = user.Name,
                Email = user.Email,
            };
        }
    }
}
