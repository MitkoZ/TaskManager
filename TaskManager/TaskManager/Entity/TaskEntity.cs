using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entity
{
    class TaskEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public int AssigneeID { get; set; }
        public int ParentUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public bool IsDone { get; set; }
    }
}
