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
            
        }
    }
}
