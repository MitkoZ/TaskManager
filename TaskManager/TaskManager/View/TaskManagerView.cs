using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;
using TaskManager.Repository;
using TaskManager.Tools;

namespace TaskManager.View
{
    class TaskManagerView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                TaskManagerViewEnum choice=RenderMenu();
                switch (choice)
                {
                    case TaskManagerViewEnum.CreatedTasks:
                        {
                            ViewCreated();
                            break;
                        }
                    case TaskManagerViewEnum.ToMakeTasks:
                        {
                            ViewToMake();
                            break;
                        }
                    case TaskManagerViewEnum.CountTime:
                        {
                            CountTime();
                            break;
                        }
                    case TaskManagerViewEnum.ChangeStatusCreated:
                        {
                            ChangeStatusCreated();
                            break;
                        }
                    case TaskManagerViewEnum.ChangeStatusToMake:
                        {
                            ChangeStatusToMake();
                            break;
                        }
                    case TaskManagerViewEnum.MakeComment:
                        {
                            MakeComment();
                            break;
                        }
                    case TaskManagerViewEnum.ViewComments:
                        {
                            ViewComments();
                            break;
                        }
                    case TaskManagerViewEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }

        private void CountTime()
        {
            ViewToMake();
            Console.Write("Please enter task id: ");
            int idInput = Int32.Parse(Console.ReadLine());
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> shouldMakeTasks = taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id);
            for (int i = 0; i < shouldMakeTasks.Count; i++)
            {
                if (shouldMakeTasks[i].Id==idInput)
                {
                    Console.Clear();
                    Console.Write("Please enter time spent on the task: ");
                    int time = Int32.Parse(Console.ReadLine());
                    TimeCountEntity timeCountEntity = new TimeCountEntity();
                    timeCountEntity.TaskId = idInput;
                    timeCountEntity.TimeSpent = time;
                    timeCountEntity.TimeCountDate = DateTime.Now;
                    timeCountEntity.ParentUserId = AuthenticationService.LoggedUser.Id;
                    TimeRepository timeRepo = new TimeRepository("time.txt");
                    timeRepo.Save(timeCountEntity);
                    Console.WriteLine("Time saved successfully");
                    Console.ReadKey(true);
                }     
            }
        }

        private TaskManagerViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tasks that I [C]reated");
                Console.WriteLine("Tasks that I should [M]ake");
                Console.WriteLine("Count [T]ime for a task");
                Console.WriteLine("Change the status for a created task (from done to [N]ot done)");
                Console.WriteLine("Change the status for a task that you should make (from not done to [D]one)");
                Console.WriteLine("Make a C[o]mment");
                Console.WriteLine("Vi[e]w a Comment");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "C":
                        {
                            return TaskManagerViewEnum.CreatedTasks;
                        }
                    case "M":
                        {
                            return TaskManagerViewEnum.ToMakeTasks;
                        }
                    case "T":
                        {
                            return TaskManagerViewEnum.CountTime;
                        }
                    case "N":
                        {
                            return TaskManagerViewEnum.ChangeStatusCreated;
                        }
                    case "D":
                        {
                            return TaskManagerViewEnum.ChangeStatusToMake;
                        }
                    case "O":
                        {
                            return TaskManagerViewEnum.MakeComment;
                        }
                    case "E":
                        {
                            return TaskManagerViewEnum.ViewComments;
                        }
                    case "X":
                        {
                            return TaskManagerViewEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            Console.ReadKey(true);
                            break;                           
                        }
                }
            }
        }
        private void ViewToMake() //the tasks that the user has to do
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> shouldMakeTasks = taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id);
            if (shouldMakeTasks.Count == 0)
            {
                Console.WriteLine("You don't have any tasks to do");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Tasks To Make");
            foreach (TaskEntity task in shouldMakeTasks)
            {
                Console.WriteLine("Id: {0}",task.Id);
                Console.WriteLine("Name: {0}", task.Name);
                Console.WriteLine("Description: {0}", task.Description);
                Console.WriteLine("Estimated time: {0}", task.EstimatedTime);
                Console.WriteLine("The person that gave me the task(id): {0}", task.ParentUserId);
                Console.WriteLine("Date created: {0}", task.DateCreated);
                Console.WriteLine("Date last updated: {0}", task.DateLastUpdated);
                Console.WriteLine("Is done? {0}", task.IsDone);
                Console.WriteLine("########################################################################");
            }
            Console.ReadKey(true);
        }

        private void ViewCreated() //the tasks that the user has created
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> createdTasks = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            if (createdTasks.Count == 0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Created Tasks");
            foreach (TaskEntity task in createdTasks)
            {
                Console.WriteLine("Id: {0}",task.Id);
                Console.WriteLine("Name: {0}",task.Name);
                Console.WriteLine("Description: {0}", task.Description);
                Console.WriteLine("Estimated time: {0}", task.EstimatedTime);
                Console.WriteLine("The person that will make it(id): {0}",task.MakerID);
                Console.WriteLine("Date created: {0}",task.DateCreated);
                Console.WriteLine("Date last updated: {0}", task.DateLastUpdated);
                Console.WriteLine("Is done? {0}", task.IsDone);
                Console.WriteLine("########################################################################");
            }
            Console.ReadKey(true);
        }

        public void ChangeStatusCreated() //changes the status of a task that has been created by the user to "not done"
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> createdTasks = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            if (createdTasks.Count==0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
            ViewCreated();
            Console.Write("Please enter the id of the task that you would want to change the status of: ");
            int idInput=Int32.Parse(Console.ReadLine());
            for (int i = 0; i < createdTasks.Count; i++)
            {
                if (createdTasks[i].Id == idInput)
                {
                    if (createdTasks[i].IsDone)
                    {
                        createdTasks[i].IsDone = false;
                        Console.WriteLine("Please write a comment:");
                        string comment = Console.ReadLine();
                        CommentEntity commentEntity = new CommentEntity();
                        commentEntity.Comment = comment;
                        commentEntity.TaskId = idInput;
                        commentEntity.ParentUserId = AuthenticationService.LoggedUser.Id;
                        CommentRepository commentRepo = new CommentRepository("comments.txt");
                        commentRepo.Save(commentEntity);
                        taskRepo.Save(createdTasks[i]);
                        Console.WriteLine("Comment saved successfully!");
                        Console.ReadKey(true);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("This task is not done");
                        Console.ReadKey(true);
                        return;
                    }    
                }
            }
            if (true)
            {
                Console.WriteLine("Invalid task id");
                Console.ReadKey(true);
                return;
            }
        }

        public void ChangeStatusToMake() //changes the status of a task that the user has to make to "done"
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> toMakeTasks = taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id);
            if (toMakeTasks.Count == 0)
            {
                Console.WriteLine("You don't have any tasks to make");
                Console.ReadKey(true);
                return;
            }
            ViewToMake();
            Console.Write("Please enter the id of the task that you would want to change the status of: ");
            int idInput = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < toMakeTasks.Count; i++)
            {
                if (toMakeTasks[i].Id == idInput)
                {
                    if (toMakeTasks[i].IsDone==false)
                    {
                        toMakeTasks[i].IsDone = true;
                        Console.WriteLine("Please write a comment:");
                        string comment = Console.ReadLine();
                        CommentEntity commentEntity = new CommentEntity();
                        commentEntity.Comment = comment;
                        commentEntity.TaskId = idInput;
                        commentEntity.ParentUserId = AuthenticationService.LoggedUser.Id;
                        CommentRepository commentRepo = new CommentRepository("comments.txt");
                        commentRepo.Save(commentEntity);
                        taskRepo.Save(toMakeTasks[i]);
                        Console.WriteLine("Comment saved successfully!");
                        Console.ReadKey(true);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("This task is already done");
                        Console.ReadKey(true);
                        return;
                    }
                }
            }
            if (true)
            {
                Console.WriteLine("Invalid task id");
                Console.ReadKey(true);
                return;
            }
        }


        public void MakeComment()
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> tasksList = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            tasksList.AddRange(taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id));
            foreach (TaskEntity task in tasksList)
            {
                Console.WriteLine("Task id: "+task.Id);
                Console.WriteLine("Task name: "+task.Name);
                Console.WriteLine("#####################################");
            }
            Console.Write("Please enter task id: ");
            int idInput = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < tasksList.Count; i++)
            {
                if (tasksList[i].Id==idInput)
                {
                    CommentRepository commentRepo = new CommentRepository("comments.txt");
                    CommentEntity commentEntity = new CommentEntity();
                    commentEntity.ParentUserId = AuthenticationService.LoggedUser.Id;
                    commentEntity.TaskId = idInput;
                    Console.WriteLine("Please write a comment:");
                    commentEntity.Comment = Console.ReadLine();
                    commentRepo.Save(commentEntity);
                    Console.WriteLine("Comment saved successfully");
                    Console.ReadKey(true);
                    return;
                }
            }
            if (true)
            {
                Console.WriteLine("Invalid task id");
                Console.ReadKey(true);
                return;
            }
        }

        public void ViewComments()
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> myTasksList = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            myTasksList.AddRange(taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id));
            CommentRepository commentRepo = new CommentRepository("comments.txt");
            List<CommentEntity> allCommentsList=commentRepo.GetAll();
            if (myTasksList.Count!=0)
            {
                foreach (TaskEntity task in myTasksList)
                {
                    Console.WriteLine(task.Id);
                    Console.WriteLine(task.Name);
                    Console.WriteLine("################################");
                }
            }
            else
            {
                Console.WriteLine("You don't have any tasks");
                Console.ReadKey(true);
                return;
            }
            Console.Write("Please enter task id: ");
            int idInput = Int32.Parse(Console.ReadLine());
            bool idExists = false;
            for (int i = 0; i < myTasksList.Count; i++)
            {
                if (myTasksList[i].Id==idInput)
                {
                    idExists = true;
                    break;
                }
            }
            if (idExists==false)
            {
                Console.WriteLine("Invalid task id");
                Console.ReadKey(true);
                return;
            }
            List<CommentEntity> currentTaskComments = new List<CommentEntity>();
            CommentEntity commentEntity = new CommentEntity();
            FileStream fileStream = new FileStream("comments.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
            { 
                commentRepo.PopulateEntity(commentEntity, streamReader);
                if (commentEntity.TaskId==idInput)
                {
                    currentTaskComments.Add(new CommentEntity { ParentUserId = commentEntity.ParentUserId, Comment = commentEntity.Comment });
                }
            }
            if (currentTaskComments.Count==0)
            {
                Console.WriteLine("This tasks doesn't have any comments");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            foreach (CommentEntity comment in currentTaskComments)
            {
                Console.WriteLine("Commenter id: "+comment.ParentUserId);
                Console.WriteLine("Comment: "+comment.Comment);
            }
            Console.ReadKey(true);
            fileStream.Close();
            streamReader.Close();
        }
    }
}
