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

        public static bool Register(TUser newUser)
        {
            using (TestDbEntities db = new TestDbEntities())
            {
                db.TUsers.Add(newUser);
                return db.SaveChanges() > 0;
            }
        }

        public static Task<bool> RegisterAsync(TUser newUser)
        {
            return Task.Run(() =>
            {
                return Register(newUser);
            });
        }
    }
}
