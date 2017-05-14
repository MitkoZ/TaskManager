using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class TaskRepository : BaseRepository<TaskEntity>
    {
        public TaskRepository(string filePath) : base(filePath)
        {
        }

        public override void WriteEntity(TaskEntity item, StreamWriter streamWriter)
        {
            streamWriter.WriteLine(item.Id);
            streamWriter.WriteLine(item.Name);
            streamWriter.WriteLine(item.Description);
            streamWriter.WriteLine(item.EstimatedTime);
            streamWriter.WriteLine(item.AssigneeID);
            streamWriter.WriteLine(item.ParentUserId);
            streamWriter.WriteLine(item.DateCreated);
            streamWriter.WriteLine(item.DateLastUpdated);
            streamWriter.WriteLine(item.IsDone);
        }

        public override void PopulateEntity(TaskEntity item, StreamReader streamReader)
        {
            item.Id = Int32.Parse(streamReader.ReadLine());
            item.Name = streamReader.ReadLine();
            item.Description = streamReader.ReadLine();
            item.EstimatedTime = Int32.Parse(streamReader.ReadLine());
            item.AssigneeID = Int32.Parse(streamReader.ReadLine());
            item.ParentUserId = Int32.Parse(streamReader.ReadLine());
            item.DateCreated = Convert.ToDateTime(streamReader.ReadLine());
            item.DateLastUpdated = Convert.ToDateTime(streamReader.ReadLine());
            item.IsDone = Convert.ToBoolean(streamReader.ReadLine());
        }
        //comment and taskrepo getall
        //public List<TaskEntity> GetAll(int parentUserId) //returns the created tasks by me
        //{
        ////    Action a = () => Console.WriteLine("test");

        ////    a();

        //    List<TaskEntity> result = new List<TaskEntity>();
        //    FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
        //    StreamReader streamReader = new StreamReader(fileStream);
        //    try
        //    {
        //        while (!streamReader.EndOfStream)
        //        {
        //            TaskEntity task = new TaskEntity();
        //            PopulateEntity(task, streamReader);
        //            if (task.ParentUserId == parentUserId)
        //            {
        //                result.Add(task);
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        streamReader.Close();
        //        fileStream.Close();
        //    }
        //    return result;
        //}

        public List<TaskEntity> GetAllToMake(int parentUserId) //returns the tasks that I should make
        {
            List<TaskEntity> result = new List<TaskEntity>();
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            try
            {
                while (!streamReader.EndOfStream)
                {
                    TaskEntity task = new TaskEntity();
                    PopulateEntity(task, streamReader);
                    if (task.AssigneeID == parentUserId)
                    {
                        result.Add(task);
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
    }
}
