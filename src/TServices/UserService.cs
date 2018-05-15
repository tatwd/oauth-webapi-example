using System;
using System.Linq;
using System.Threading.Tasks;
using TModel;

namespace TServices
{
    public class UserService
    {
        public static bool Login(string username, string password)
        {
            using (TestDbEntities db = new TestDbEntities())
            {
                return db.TUsers.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                    && user.Password == password);
            }
        }

        public static Task<TUser> LoginAsync(string username, string password)
        {
            return Task.Run(() => 
            {
                using (TestDbEntities db = new TestDbEntities())
                {
                    return db.TUsers
                        .Where(user => user.Username == username && user.Password == password)
                        .SingleOrDefault();
                }
            });
        }
    }
}
