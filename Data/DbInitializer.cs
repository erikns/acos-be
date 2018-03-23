using System.Linq;
using ACOS_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACOS_be.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.Migrate();
            Seed(context);
        }

        public static void Seed(ApplicationContext context)
        {
            TaskType workType = new TaskType
            {
                Name = "Jobb"
            };
            TaskType adminType = new TaskType
            {
                Name = "Administrasjon"
            };

            if (!TypeExistsInDb(context.TaskTypes, workType.Name)) context.Add(workType);
            if (!TypeExistsInDb(context.TaskTypes, adminType.Name)) context.Add(adminType);

            User user1 = new User
            {
                Name = "Bruker Brukersen",
                Email = "bruker@brukersen.no"
            };
            User user2 = new User
            {
                Name = "Test Testesen",
                Email = "test@testesen.no"
            };

            if (!EmailExistsInDb(context.Users, user1.Email)) context.Add(user1);
            if (!EmailExistsInDb(context.Users, user2.Email)) context.Add(user2);

            context.SaveChanges();
        }

        private static bool EmailExistsInDb(DbSet<User> set, string email)
        {
            if (set.Where(u => u.Email == email).Count() == 1) return true;
            else return false;
        }

        private static bool TypeExistsInDb(DbSet<TaskType> set, string name)
        {
            if (set.Where(t => t.Name == name).Count() == 1) return true;
            else return false;
        }
    }
}