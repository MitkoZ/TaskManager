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

        public List<T> GetAll(Predicate<T> filter = null)
        {
            List<T> result = new List<T>();
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    T item = new T();
                    PopulateEntity(item, streamReader);

                    if (filter == null || filter(item) == true)
                    {
                        result.Add(item);
                    }
                }
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }
            return result;
        }

        public void Delete(T item)
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
                    PopulateEntity(itemDatabase,streamReader);
                    if (itemDatabase.Id != item.Id)
                    {
                        WriteEntity(itemDatabase, streamWriter);
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

        private void Insert(T item)
        {
            item.Id = GetNextId();
            FileStream fileStream = new FileStream(filePath, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            try
            {
                WriteEntity(item, streamWriter);
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }
        }

        private void Update(T oldItem)
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

        public T GetById(int id)
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    T itemDatabase = new T();
                    PopulateEntity(itemDatabase, streamReader);
                    if (id == itemDatabase.Id)
                    {
                        return itemDatabase;
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

        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}
