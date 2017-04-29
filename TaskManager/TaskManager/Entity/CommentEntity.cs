using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entity
{
    class CommentEntity:BaseEntity
    {
        public int TaskId { get; set; }
        public int ParentUserId { get; set; }
        public string Comment { get; set; }
    }
}
