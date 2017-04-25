using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class UsersRepository:BaseRepository<User>
    {
        public UsersRepository(string filePath):base(filePath)
        { 
        }

        protected override void WriteEntity(User item)
        {
            item.Id = GetNextId();
            FileStream stream = new FileStream(this.filePath, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine("\r\n" + item.Id);
            streamWriter.WriteLine(item.Username);
            streamWriter.WriteLine(item.Password);
            streamWriter.WriteLine(item.isAdmin);
            streamWriter.Close();
            TrailingSpaceRemove(this.filePath);
        }

        protected override void PopulateEntity(User item, StreamReader streamReader)
        {
            item.Id = Int32.Parse(streamReader.ReadLine());
            item.Username = streamReader.ReadLine();
            item.Password = streamReader.ReadLine();
            item.isAdmin = Convert.ToBoolean(streamReader.ReadLine());
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    User user = new User();
                    PopulateEntity(user,streamReader);
                    if (user.Username==username && user.Password==password)
                    {
                        return user;
                    }
                }
            }
            finally
            {
                fileStream.Close();
                streamReader.Close();
            }
            return null;
        }

        public bool UserExist(string username)
        {
            StreamReader streamReader = new StreamReader(filePath);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    User userDatabase = new User();
                    PopulateEntity(userDatabase, streamReader);
                    if (userDatabase.Username == username)
                    {
                        return true;// a user with the same username already exists
                    }
                }
            }
            finally
            {
                streamReader.Close();
            }
            return false;
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
            bool userExist = UserExist(userInput.Username);
            if (userExist)
            {
                Console.WriteLine("A user with the same username already exists!");
                return;
            }
            else
            {
                WriteEntity(userInput);
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
