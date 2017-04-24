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
                User userInput = new Entity.User();
                Console.Write("Username: ");
                userInput.Username = Console.ReadLine();
                Console.Write("Password: ");
                userInput.Password = Console.ReadLine();

                AuthenticationService.AuthenticateUser(userInput.Username, userInput.Password);

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
