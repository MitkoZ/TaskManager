using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

using TaskManager.Repository;

namespace TaskManager
{
    public class AuthenticationService
    {
        public static User LoggedUser { get; private set; }
        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            List<User> result = userRepo.GetAll(u => u.Username == username && u.Password == password);
            LoggedUser = result.Count > 0 ? result[0] : null;
        }
    }
}
