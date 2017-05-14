using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;
using TaskManager.Repository;
using TaskManager.View;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("TEST!!!");

            //UsersRepository repo = new UsersRepository("users.txt");

            ////Predicate<User> dlg = delegate (User u)
            ////{
            ////    return u.Username == "admin";
            ////};

            //List<User> result = repo.GetAll(u => u.Username == "admin");

            //foreach(User u in result)
            //{
            //    Console.WriteLine(u.Username);
            //}

            //Console.ReadKey(true);


            LoginView loginView = new LoginView();
            loginView.Show();
            if (AuthenticationService.LoggedUser.isAdmin)
            {
                AdminFrontView adminFrontView = new AdminFrontView();
                adminFrontView.Show();
            }
            else
            {
                UserView userView = new UserView();
                userView.Show();
            }
        }
    }
}
