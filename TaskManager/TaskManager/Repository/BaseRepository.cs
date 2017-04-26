using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class BaseRepository<T> where T:BaseEntity, new()
    {
        protected readonly string filePath;
        
        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void Update(T oldItem)
        {
            string tempFilePath = "temp." + filePath;
            FileStream inputFileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(inputFileStream);
            FileStream outputFileStream = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(outputFileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T itemDatabase = new T();
                    PopulateEntity(itemDatabase, streamReader);
                    if (itemDatabase.Id != oldItem.Id)
                    {
                        WriteEntity(itemDatabase, streamWriter);
                    }
                    else
                    {
                        WriteEntity(oldItem, streamWriter);
                    }
                }
            }
            finally
            {
                streamWriter.Close();
                outputFileStream.Close();
                streamReader.Close();
                inputFileStream.Close();
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public virtual void PopulateEntity(T item, StreamReader streamReader)
        {
        }

        public virtual void WriteEntity(T item, StreamWriter streamWriter)
        {
        }

        public int GetNextId()
        {
            FileStream fileStream = new FileStream(this.filePath,FileMode.OpenOrCreate);
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
    }
}
