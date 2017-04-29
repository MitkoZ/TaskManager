using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class TimeRepository : BaseRepository<TimeCountEntity>
    {
        public TimeRepository(string filePath) : base(filePath)
        {
        }

        public override void PopulateEntity(TimeCountEntity item, StreamReader streamReader)
        {
            item.Id = Int32.Parse(streamReader.ReadLine());
            item.TaskId = Int32.Parse(streamReader.ReadLine());
            item.ParentUserId = Int32.Parse(streamReader.ReadLine());
            item.TimeSpent = Int32.Parse(streamReader.ReadLine());
            item.TimeCountDate = Convert.ToDateTime(streamReader.ReadLine());
        }

        public override void WriteEntity(TimeCountEntity item, StreamWriter streamWriter)
        {
            streamWriter.WriteLine(item.Id);
            streamWriter.WriteLine(item.TaskId);
            streamWriter.WriteLine(item.ParentUserId);
            streamWriter.WriteLine(item.TimeSpent);
            streamWriter.WriteLine(item.TimeCountDate);
        }
    }
}
