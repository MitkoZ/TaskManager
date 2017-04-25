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
            UsersRepository adminRepo = new UsersRepository("users.txt");
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
                case AdminViewEnum.Exit:
                    {
                        return;
                    }
            }
            Console.ReadKey(true);
        }

         public void ShowEdit()
         {
           AdminRepoEditEnum choice = RenderEditMenu();//todo
            switch (choice)
            {
                case AdminRepoEditEnum.Usename:
                    {
                        break;
                    }
                    
                case AdminRepoEditEnum.Password:
                    {
                        break;
                    }
                    
                case AdminRepoEditEnum.Privileges:
                    {
                        break;
                    }
                    
                case AdminRepoEditEnum.Exit:
                    {
                        return;
                    }
            }
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

        private AdminRepoEditEnum RenderEditMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Change [u]sername");
                Console.WriteLine("Change [p]assword");
                Console.WriteLine("Change p[r]ivileges");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "U":
                        {
                            return AdminRepoEditEnum.Usename;
                        }
                    case "P":
                        {
                            return AdminRepoEditEnum.Password;
                        }
                    case "R":
                        {
                            return AdminRepoEditEnum.Privileges;
                        }
                    case "X":
                        {
                            return AdminRepoEditEnum.Exit;
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
