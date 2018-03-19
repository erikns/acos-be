using Microsoft.EntityFrameworkCore;

namespace ACOS_be.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.Migrate();
        }
    }
}