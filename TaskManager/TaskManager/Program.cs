using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.View;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            if (AuthenticationService.LoggedUser.isAdmin)
            {
                AdminView adminView = new AdminView();
                adminView.Show();
            }
            else
            {
                UserView userView = new UserView();
                userView.Show();
            }
        }
    }
}
