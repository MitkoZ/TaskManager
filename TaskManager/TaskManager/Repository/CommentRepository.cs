using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.Repository
{
    class CommentRepository : BaseRepository<CommentEntity>
    {
        public CommentRepository(string filePath) : base(filePath)
        {
        }

        public override void PopulateEntity(CommentEntity item, StreamReader streamReader)
        {
            item.Id = Int32.Parse(streamReader.ReadLine());
            item.TaskId = Int32.Parse(streamReader.ReadLine());
            item.ParentUserId = Int32.Parse(streamReader.ReadLine());
            item.Comment = streamReader.ReadLine();
        }

        public override void WriteEntity(CommentEntity item, StreamWriter streamWriter)
        {
            streamWriter.WriteLine(item.Id);
            streamWriter.WriteLine(item.TaskId);
            streamWriter.WriteLine(item.ParentUserId);
            streamWriter.WriteLine(item.Comment);
        }
    }
}
