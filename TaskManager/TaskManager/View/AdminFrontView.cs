using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Tools;

namespace TaskManager.View
{
    class AdminFrontView
    {
        public void Show()
        {
            while (true)
            {
                AdminFrontViewEnum choice = RenderMenu();
                switch (choice)
                {
                    case AdminFrontViewEnum.AdminView:
                        {
                            AdminView adminView = new AdminView();
                            adminView.Show();
                            break;
                        }
                    case AdminFrontViewEnum.OrdinaryUserView:
                        {
                            UserView userView = new UserView();
                            userView.Show();
                            break;
                        }
                    case AdminFrontViewEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }

        private AdminFrontViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[A]dmin view");
                Console.WriteLine("[O]rdinary user view");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "A":
                        {
                            return AdminFrontViewEnum.AdminView;
                        }
                    case "O":
                        {
                            return AdminFrontViewEnum.OrdinaryUserView;
                        }
                    case "X":
                        {
                            return AdminFrontViewEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
    }
}
