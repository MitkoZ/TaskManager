using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Tools
{
    public enum AdminFrontViewEnum
    {
        AdminView=1,
        OrdinaryUserView = 2,
        Exit = 3
    }

    public enum AdminViewEnum
    {
        Add=1,
        Update=2,
        Delete=3,
        View=4,
        GetAll=5,
        Exit = 6
    }

    public enum UserViewEnum
    {
        TaskManagement=1,
        CommentManagement=2,
        Exit=3
    }

    public enum TaskManagementViewEnum
    {
        Add = 1,
        Delete = 2,
        Update = 3,
        CreatedTasks =4,
        ToMakeTasks=5,
        CountTime=6,
        ChangeStatusCreated=7,
        ChangeStatusToMake=8,
        Exit =9
    }

    public enum CommentManagementViewEnum
    {
        MakeComment = 1,
        ViewComments = 2,
        Exit = 3
    }
}
