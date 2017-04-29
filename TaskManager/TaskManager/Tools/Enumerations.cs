using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Tools
{
    public enum AdminViewEnum
    {
        Add=1,
        Update=2,
        Delete=3,
        View=4,
        Exit=5,
        GetAll=6
    }

    public enum UserViewEnum
    {
        MyTasks=1,
        Add=2,
        Delete=3,
        Update=4,
        Exit=5
    }

    public enum TaskManagerViewEnum
    {
        CreatedTasks=1,
        ShouldMakeTasks=2,
        Exit=3
    }
}
