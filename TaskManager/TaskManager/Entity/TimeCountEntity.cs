using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entity
{
    class TimeCountEntity:BaseEntity
    {
        public int TaskId { get; set; }
        public int ParentUserId { get; set; }
        public int TimeSpent { get; set; }
        public DateTime TimeCountDate { get; set; }
    }
}
