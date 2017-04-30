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
        GetAll=6,
        OrdinaryUserView=7
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
        ToMakeTasks=2,
        CountTime=3,
        ChangeStatusCreated=4,
        ChangeStatusToMake=5,
        MakeComment=6,
        ViewComments=7,
        Exit =8
    }
}
