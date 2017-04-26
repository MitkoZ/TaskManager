using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;
using TaskManager.Repository;
using TaskManager.Tools;
namespace TaskManager.View
{
    class AdminView : UserView
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
                        Add();
                        break;
                    }
                case AdminViewEnum.Update:
                    {
                        Update();
                        break;
                    }
                case AdminViewEnum.Delete:
                    {
                        Delete();
                        break;
                    }
                case AdminViewEnum.View:
                    {
                        View();
                        break;
                    }
                case AdminViewEnum.Exit:
                    {
                        return;
                    }
            }
            Console.ReadKey(true);
        }

        private void Delete()
        {

        }

        private void Update() //by username
        {
            Console.Clear();
            UsersRepository adminRepo = new UsersRepository("users.txt");
            Console.Write("Enter user username: ");
            string usernameInput = Console.ReadLine();
            Console.Clear();
            User oldUser = adminRepo.View(usernameInput);
            Console.Write("Enter new username: ");
            User userInput = new User();
            userInput.Username=Console.ReadLine();
            Console.Write("Enter new password: ");
            userInput.Password = Console.ReadLine();
            Console.Write("Is admin?: (true or false) ");
            string isAdmin = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput.Username))
            {
                oldUser.Username = userInput.Username;
            }
            if (!string.IsNullOrEmpty(userInput.Password))
            {
                oldUser.Password = userInput.Password;
            }
            if (!string.IsNullOrEmpty(isAdmin) && isAdmin.ToLower() == "true"|| isAdmin=="false")
            {
                oldUser.isAdmin = Convert.ToBoolean(isAdmin);
            }
            adminRepo.Update(oldUser);
            Console.WriteLine("User changed!");
        }

        private void Add()
        { 
            User userInput = new Entity.User();
            Console.Write("Enter username: ");
            userInput.Username = Console.ReadLine();
            Console.Write("Enter password: ");
            userInput.Password = Console.ReadLine();
            Console.Write("Admin? (true or false)");
            userInput.isAdmin = Convert.ToBoolean(Console.ReadLine());
            UsersRepository adminRepo = new UsersRepository("users.txt");
            bool userExist = adminRepo.UserExist(userInput.Username);
            if (userExist)
            {
                Console.WriteLine("A user with the same username already exists!");
                return;
            }
            else
            {
                userInput.Id= adminRepo.GetNextId();
                FileStream fileStream = new FileStream("users.txt", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                adminRepo.WriteEntity(userInput, streamWriter);
                Console.WriteLine("User added!");
                streamWriter.Close();
            }
        }

        private void View()
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            Console.Write("Enter user username: ");
            string usernameInput = Console.ReadLine();
            bool userExist = userRepo.UserExist(usernameInput);
            if (userExist)
            {
                User user = userRepo.GetByUsername(usernameInput);
                Console.WriteLine("Id " + user.Id);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("Password: " + user.Password);
                Console.WriteLine("Is Admin?: " + user.isAdmin);
            }
            else
            {
                Console.WriteLine("This user doesn't exist");
                Console.ReadKey(true);
                return;
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
                            return AdminViewEnum.Update;
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
    }
}
