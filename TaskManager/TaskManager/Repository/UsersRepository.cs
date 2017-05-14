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

        public override void WriteEntity(User item, StreamWriter streamWriter)
        {
            streamWriter.WriteLine(item.Id);
            streamWriter.WriteLine(item.Username);
            streamWriter.WriteLine(item.Password);
            streamWriter.WriteLine(item.isAdmin);
        }

        public override void PopulateEntity(User item, StreamReader streamReader)
        {
            item.Id = Int32.Parse(streamReader.ReadLine());
            item.Username = streamReader.ReadLine();
            item.Password = streamReader.ReadLine();
            item.isAdmin = Convert.ToBoolean(streamReader.ReadLine());
        }
    }
}
