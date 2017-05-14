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
    class TaskManagementView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                TaskManagementViewEnum choice=RenderMenu();
                switch (choice)
                {
                    case TaskManagementViewEnum.Add:
                        {
                            Add();
                            break;
                        }
                    case TaskManagementViewEnum.Delete:
                        {
                            Delete();
                            break;
                        }
                    case TaskManagementViewEnum.Update:
                        {
                            Update();
                            break;
                        }
                    case TaskManagementViewEnum.CreatedTasks:
                        {
                            ViewCreated();
                            break;
                        }
                    case TaskManagementViewEnum.ToMakeTasks:
                        {
                            ViewToMake();
                            break;
                        }
                    case TaskManagementViewEnum.CountTime:
                        {
                            CountTime();
                            break;
                        }
                    case TaskManagementViewEnum.ChangeStatusCreated:
                        {
                            ChangeStatusCreated();
                            break;
                        }
                    case TaskManagementViewEnum.ChangeStatusToMake:
                        {
                            ChangeStatusToMake();
                            break;
                        }
                    case TaskManagementViewEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }

        private void CountTime()
        {
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> shouldMakeTasks = taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id);
            if (shouldMakeTasks.Count==0)
            {
                Console.WriteLine("You don't have any tasks to make");
                Console.ReadKey(true);
                return;
            }
            ViewToMake();
            Console.Write("Please enter task id: ");
            int idInput = 0;
            try
            {
                idInput = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            if (!(shouldMakeTasks.Any(t=>t.Id==idInput)))
            {
                Console.WriteLine("You don't have a task with this id");
                Console.ReadKey(true);
                return;
            }
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

        private TaskManagementViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Task Management");
                Console.WriteLine("C[r]eate a task");
                Console.WriteLine("D[e]lete a task");
                Console.WriteLine("[U]pdate a task");
                Console.WriteLine("Tasks that I [C]reated");
                Console.WriteLine("Tasks that I should [M]ake");
                Console.WriteLine("Count [T]ime for a task");
                Console.WriteLine("Change the status for a created task (from done to [N]ot done)");
                Console.WriteLine("Change the status for a task that you should make (from not done to [D]one)");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "R":
                        {
                            return TaskManagementViewEnum.Add;
                        }
                    case "E":
                        {
                            return TaskManagementViewEnum.Delete;
                        }
                    case "U":
                        {
                            return TaskManagementViewEnum.Update;
                        }
                    case "C":
                        {
                            return TaskManagementViewEnum.CreatedTasks;
                        }
                    case "M":
                        {
                            return TaskManagementViewEnum.ToMakeTasks;
                        }
                    case "T":
                        {
                            return TaskManagementViewEnum.CountTime;
                        }
                    case "N":
                        {
                            return TaskManagementViewEnum.ChangeStatusCreated;
                        }
                    case "D":
                        {
                            return TaskManagementViewEnum.ChangeStatusToMake;
                        }
                    case "X":
                        {
                            return TaskManagementViewEnum.Exit;
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
            UsersRepository userRepo = new UsersRepository("users.txt");
            Console.WriteLine("Tasks To Make");
            foreach (TaskEntity task in shouldMakeTasks)
            {
                Console.WriteLine("Id: {0}",task.Id);
                Console.WriteLine("Name: {0}", task.Name);
                Console.WriteLine("Description: {0}", task.Description);
                Console.WriteLine("Estimated time: {0}", task.EstimatedTime);
                Console.WriteLine("The person that gave me the task: {0}", userRepo.GetById(task.ParentUserId).Username);
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
            List<TaskEntity> createdTasks = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/ u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);
            if (createdTasks.Count == 0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
            UsersRepository userRepo = new UsersRepository("users.txt");
            Console.WriteLine("Created Tasks");
            foreach (TaskEntity task in createdTasks)
            {
                Console.WriteLine("Id: {0}",task.Id);
                Console.WriteLine("Name: {0}",task.Name);
                Console.WriteLine("Description: {0}", task.Description);
                Console.WriteLine("Estimated time: {0}", task.EstimatedTime);
                Console.WriteLine("The person that will make it: {0}",userRepo.GetById(task.AssigneeID).Username);
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
            List<TaskEntity> createdTasks = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/ u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);
            if (createdTasks.Count==0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
            ViewCreated();
            Console.Write("Please enter the id of the task that you would want to change the status of: ");
            int idInput = 0;
            try
            {
                idInput = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
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
            int idInput = 0;
            try
            {
                idInput = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
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

        private void Add()
        {
            Console.Clear();
            TaskEntity task = new TaskEntity();
            task.ParentUserId = AuthenticationService.LoggedUser.Id;
            Console.WriteLine("Create task: ");
            Console.Write("Please write task name: ");
            task.Name = Console.ReadLine();
            if (task.Name==string.Empty)
            {
                Console.WriteLine("Invalid task name");
                Console.ReadKey(true);
                return;
            }
            Console.Write("Please write task description: ");
            task.Description = Console.ReadLine();
            if (task.Description == string.Empty)
            {
                Console.WriteLine("Invalid task description");
                Console.ReadKey(true);
                return;
            }
            Console.Write("Please write estimated task time: (in hours) ");
            try
            {
                task.EstimatedTime = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            UsersRepository usersRepo = new UsersRepository("users.txt");
            List<User> users = usersRepo.GetAll(user => user.Id != AuthenticationService.LoggedUser.Id);
            Console.WriteLine("Users:");
            foreach (User user in users)
            {
                Console.WriteLine("ID: "+user.Id);
                Console.WriteLine("Username: "+user.Username);
                Console.WriteLine("##################################");
            }
            Console.Write("Please write the person who will make the task (by id): ");
            task.AssigneeID = Int32.Parse(Console.ReadLine());
            User userDatabase=usersRepo.GetById(task.AssigneeID);
            if (userDatabase==null)
            {
                Console.WriteLine("A user with this id doesn't exist");
                Console.ReadKey(true);
                return;
            }
            task.DateCreated = DateTime.Now;
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            taskRepo.Save(task);
            Console.WriteLine("Task saved successfully!");
            Console.ReadKey(true);
        }

        private void Delete()
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> tasksCreatedByMe = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/ u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);
            if (tasksCreatedByMe.Count==0)
            {
                Console.WriteLine("You don't have any tasks");
                Console.ReadKey(true);
                return;
            }
            ViewCreated();
            Console.WriteLine("Delete task");
            Console.Write("Please enter task id: ");
            int idInput = 0;
            try
            {
                idInput = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }  
            TaskEntity task = taskRepo.GetById(idInput);
            if (task == null)
            {
                Console.WriteLine("Task doesn't exist!");
            }
            else if (tasksCreatedByMe.Any(t => t.Id==idInput))
            {
                taskRepo.Delete(task);
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("You don't have a task with this id");
            }
            Console.ReadKey(true);
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update task");
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> myTasks = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/ u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);
            if (myTasks.Count==0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
            ViewCreated();
            Console.Write("Please enter task id: ");
            int inputId = 0;
            try
            {
                inputId = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            TaskEntity task = taskRepo.GetById(inputId);
            if (task == null)
            {
                Console.Clear();
                Console.WriteLine("Task doesn't exist!");
                Console.ReadKey(true);
                return;
            }
            if (!(myTasks.Any(t => t.Id == inputId)))
            {
                Console.WriteLine("You don't have a task with this id");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Task name: {0}", task.Name);
            Console.Write("Please enter new task name: ");
            string newName = Console.ReadLine();
            if (newName==string.Empty)
            {
                Console.WriteLine("Invalid name");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Task description: {0}", task.Description);
            Console.Write("Please enter new task description: ");
            string newDescription = Console.ReadLine();
            if (newDescription==string.Empty)
            {
                Console.WriteLine("Invalid name");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Task estimated time: {0}", task.EstimatedTime);
            Console.Write("Please enter new estimated time: ");
            int newEstimatedTime = 0;
            try
            {
                newEstimatedTime = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            UsersRepository userRepo = new UsersRepository("users.txt");
            List<User>users=userRepo.GetAll(user => user.Id != AuthenticationService.LoggedUser.Id);
            Console.WriteLine("Users:");
            foreach (User user in users)
            {
                Console.WriteLine("ID: " + user.Id);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("##################################");
            }
            Console.WriteLine("Person(id) that will make the task: {0}", task.AssigneeID);
            Console.Write("Please enter new person(id) that will make the task: ");
            int newMakerId = 0;
            try
            {
                newMakerId = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Is task done?: {0}", task.IsDone);
            Console.Write("Please enter if task is done?(true or false): ");
            bool newIsDone = false;
            try
            {
                newIsDone = Convert.ToBoolean(Console.ReadLine());
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.ReadKey(true);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
            task.Name = newName;
            task.Description = newDescription;
            task.EstimatedTime = newEstimatedTime;
            task.AssigneeID = newMakerId;
            task.IsDone = newIsDone;
            task.DateLastUpdated = DateTime.Now;
            taskRepo.Save(task);
            Console.WriteLine("Task updated successfully!");
            Console.ReadKey(true);
        }
    }
}
