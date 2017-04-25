using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class BaseRepository<T> where T:BaseEntity,new()
    {
        protected readonly string filePath;
        
        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }

        protected virtual void PopulateEntity(T item, StreamReader streamReader)
        {
        }

        protected virtual void WriteEntity(T item)
        {
        }

        protected int GetNextId()
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                int id = 1;
                while (!streamReader.EndOfStream)
                {
                    T item = new T();
                    PopulateEntity(item,streamReader);
                    if (id<=item.Id)
                    {
                        id = item.Id + 1;
                    }
                }
                return id;
            }
            finally
            {
                fileStream.Close();
                streamReader.Close();
            }
        }

        public void TrailingSpaceRemove(string filePath)
        {
            string file = File.ReadAllText(filePath).Trim();
            File.Delete(filePath);
            File.AppendAllText(filePath, file);
        }
    }
}
