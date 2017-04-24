using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class BaseRepository/*<T>*/
    {
        protected readonly string filePath;
        
        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }
        protected void PopulateEntity(/*T*/User item)
        {
            FileStream stream = new FileStream(this.filePath, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine("\r\n"+item.Username);
            streamWriter.WriteLine(item.Password);
            streamWriter.WriteLine(item.isAdmin);
            streamWriter.Close();
            TrailingSpaceRemove(this.filePath);
        }
        public bool UserExist(string username)
        {
            StreamReader streamReader = new StreamReader(filePath);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    User userDatabase = new User();
                    userDatabase.Username = streamReader.ReadLine();
                    userDatabase.Password = streamReader.ReadLine();
                    userDatabase.isAdmin = Convert.ToBoolean(streamReader.ReadLine());
                    if (userDatabase.Username==username)
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

        public void TrailingSpaceRemove(string filePath)
        {
            string file = File.ReadAllText(filePath).Trim();
            File.Delete(filePath);
            File.AppendAllText(filePath, file);
        }
    }
}
