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
            streamWriter.WriteLine(item.MakerID);
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
            item.MakerID = Int32.Parse(streamReader.ReadLine());
            item.ParentUserId = Int32.Parse(streamReader.ReadLine());
            item.DateCreated = Convert.ToDateTime(streamReader.ReadLine());
            item.DateLastUpdated = Convert.ToDateTime(streamReader.ReadLine());
            item.IsDone = Convert.ToBoolean(streamReader.ReadLine());
        }
    }
}
