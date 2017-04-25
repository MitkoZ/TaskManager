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
        Edit=2,
        Delete=3,
        View=4,
        Exit=5
    }

    public enum AdminRepoEditEnum
    {
        Usename=1,
        Password=2,
        Privileges=3,
        Exit=4
    }
}
