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
    class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                AdminViewEnum choice = RenderMenu();
                Console.Clear();
                switch (choice)
                {
                    case AdminViewEnum.GetAll:
                        {
                            GetAll();
                            break;
                        }
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
            }
        }

        private void GetAll()
        {
            Console.Clear();
            UsersRepository userRepo = new UsersRepository("users.txt");
            List<User> users = userRepo.GetAll();
            foreach (User user in users)
            {
                Console.WriteLine("Id: "+user.Id);
                Console.WriteLine("Username: "+user.Username);
                Console.WriteLine("Password: "+user.Password);
                Console.WriteLine("Is Admin: "+user.isAdmin);
            }
            Console.ReadKey(true);
        }

        private void Delete()
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            Console.Clear();
            GetAll();
            Console.WriteLine("Delete user: ");
            Console.Write("Id: ");
            int id = Int32.Parse(Console.ReadLine());
            User user = userRepo.GetById(id);
            if (user == null)
            {
                Console.WriteLine("User not found");
            }
            else
            {
                userRepo.Delete(user);
                Console.WriteLine("User deleted successfully.");
            }
            Console.ReadKey(true);
        }
        
        private void Update() //by id
        {
            Console.Clear();
            GetAll();
            UsersRepository adminRepo = new UsersRepository("users.txt");
            Console.Write("Enter user id: ");
            int idInput = Int32.Parse(Console.ReadLine());
            Console.Clear();
            User oldUser = adminRepo.GetById(idInput);
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
            adminRepo.Save(oldUser);
            Console.WriteLine("User changed!");
            Console.ReadKey(true);
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
            userInput.Id= adminRepo.GetNextId();
            FileStream fileStream = new FileStream("users.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            adminRepo.WriteEntity(userInput, streamWriter);
            Console.WriteLine("User added!");
            streamWriter.Close();
            Console.ReadKey(true);
        }

        private void View()
        {
            UsersRepository userRepo = new UsersRepository("users.txt");
            List<User> users = userRepo.GetAll();
            foreach (User item in users)
            {
                Console.WriteLine("Id: " + item.Id);
                Console.WriteLine("Username: " + item.Username);
                Console.WriteLine("#####################");
            }
            Console.Write("Enter user id: ");
            int idInput = Int32.Parse(Console.ReadLine());
            Console.Clear();
            User user = userRepo.GetById(idInput);
            Console.WriteLine("Id " + user.Id);
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("Password: " + user.Password);
            Console.WriteLine("Is Admin?: " + user.isAdmin);
            Console.ReadKey(true);
        }
        
        private AdminViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin view");
                Console.WriteLine("[G]et all users");
                Console.WriteLine("[A]dd a user");
                Console.WriteLine("[E]dit a user");
                Console.WriteLine("[D]elete a user");
                Console.WriteLine("[V]iew a user");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return AdminViewEnum.GetAll;
                        }
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
