using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class UsersRepository:BaseRepository
    {
        public UsersRepository(string filePath):base(filePath)
        { 
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
                    user.Username = streamReader.ReadLine();
                    user.Password = streamReader.ReadLine();
                    user.isAdmin = Convert.ToBoolean(streamReader.ReadLine());
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
    }
}
