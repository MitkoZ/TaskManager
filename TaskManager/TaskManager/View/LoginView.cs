using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.View
{
    class LoginView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Username: ");
                string inputUsername = Console.ReadLine();
                Console.Write("Password: ");
                string inputPassword = Console.ReadLine();
                AuthenticationService.AuthenticateUser(inputUsername, inputPassword);
                if (AuthenticationService.LoggedUser!=null)
                {
                    Console.WriteLine("Welcome "+AuthenticationService.LoggedUser.Username);
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
