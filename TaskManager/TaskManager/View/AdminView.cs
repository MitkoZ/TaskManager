using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Repository;
using TaskManager.Tools;
namespace TaskManager.View
{
    class AdminView:UserView
    {
        public override void Show()
        {
            AdminRepository adminRepo = new AdminRepository("users.txt");
            AdminViewEnum choice = RenderMenu();
            Console.Clear();
            switch (choice)
            {
                case AdminViewEnum.Add:
                    {
                        adminRepo.Add();
                        break;
                    }
                case AdminViewEnum.Edit:
                    {
                        adminRepo.Edit();
                        break;
                    }
                case AdminViewEnum.Delete:
                    {
                        adminRepo.Delete();
                        break;
                    }
                case AdminViewEnum.View:
                    {
                        adminRepo.View();
                        break;
                    }
                case AdminViewEnum.MakeAdmin:
                    {
                        adminRepo.MakeAdmin();
                        break;
                    }
                case AdminViewEnum.Exit:
                    {
                        return;
                    }
            }
            Console.ReadKey(true);
        }

        private AdminViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin view");
                Console.WriteLine("[A]dd a user");
                Console.WriteLine("[E]dit a user");
                Console.WriteLine("[D]elete a user");
                Console.WriteLine("[V]iew a user");
                Console.WriteLine("[M]ake a user to admin");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "A":
                        {
                            return AdminViewEnum.Add;
                        }
                    case "E":
                        {
                            return AdminViewEnum.Edit;
                        }
                    case "D":
                        {
                            return AdminViewEnum.Delete;
                        }
                    case "V":
                        {
                            return AdminViewEnum.View;
                        }
                    case "M":
                        {
                            return AdminViewEnum.MakeAdmin;
                        }
                    case "X":
                        {
                            return AdminViewEnum.Exit;
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
