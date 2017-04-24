using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class AdminRepository:BaseRepository/*<User>*/
    {
        public AdminRepository(string filePath):base(filePath)
        {

        }
        public void Add()
        {
            User userInput = new Entity.User();
            Console.Write("Enter username: ");
            userInput.Username = Console.ReadLine();
            Console.Write("Enter password: ");
            userInput.Password = Console.ReadLine();
            Console.Write("Admin? (true or false)");
            userInput.isAdmin = Convert.ToBoolean(Console.ReadLine());
            bool userExist=UserExist(userInput.Username);
            if (userExist)
            {
                Console.WriteLine("A user with the same username already exists!");
                return;
            }
            else
            {
                PopulateEntity(userInput);
                Console.WriteLine("User added!");
            }
        }

        public void Edit()
        {

        }

        public void Delete()
        {

        }

        public void View()
        {

        }

        public void MakeAdmin()
        {

        }
    }
}
