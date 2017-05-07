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
