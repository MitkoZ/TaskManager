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
    class UserView
    {
        public void Show()
        {
            Console.Clear();
            while (true)
            {
                UserViewEnum choice = RenderMenu();
                switch (choice)
                {
                    case UserViewEnum.MyTasks:
                        {
                            TaskManagerView taskManagerView = new TaskManagerView();
                            taskManagerView.Show();
                            break;
                        }
                    case UserViewEnum.Add:
                        {
                            Add();
                            break;
                        }
                    case UserViewEnum.Update:
                        {
                            Update();
                            break;
                        }
                    case UserViewEnum.Delete:
                        {
                            Delete();
                            break;
                        }
                    case UserViewEnum.Exit:
                        {
                            return;
                        }
                }
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
            Console.Write("Please write task description: ");
            task.Description = Console.ReadLine();
            Console.Write("Please write estimated task time: (in hours) ");
            task.EstimatedTime = Int32.Parse(Console.ReadLine());
            Console.Write("Please write the person who will make the task (by id): ");
            task.MakerID = Int32.Parse(Console.ReadLine());
            task.DateCreated = DateTime.Now;
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            taskRepo.Save(task);
            Console.WriteLine("Task saved successfully!");
            Console.ReadKey(true);
        }

        private void Delete()
        {
            Console.Clear();
            Console.WriteLine("Delete task");
            Console.Write("Please enter task id: ");
            int inputId=Int32.Parse(Console.ReadLine());
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            TaskEntity task = taskRepo.GetById(inputId);
            if (task==null)
            {
                Console.WriteLine("Task doesn't exist!");
            }
            else
            {
                taskRepo.Delete(task);
                Console.WriteLine("Task deleted successfully!");
            }
            Console.ReadKey(true);
        }

        private void Update()
        {
            Console.Clear();
            Console.WriteLine("Update task");
            Console.Write("Please enter task id: ");
            int inputId = Int32.Parse(Console.ReadLine());
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            TaskEntity task = taskRepo.GetById(inputId);
            if (task==null)
            {
                Console.Clear();
                Console.WriteLine("Task doesn't exist!");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Task name: {0}",task.Name);
            Console.Write("Please enter new task name: ");
            string newName = Console.ReadLine();
            Console.WriteLine("Task description: {0}",task.Description);
            Console.Write("Please enter new task description: ");
            string newDescription = Console.ReadLine();
            Console.WriteLine("Task estimated time: {0}",task.EstimatedTime);
            Console.Write("Please enter new estimated time: ");
            string newEstimatedTime = Console.ReadLine();
            Console.WriteLine("Person(id) that will make the task: {0}",task.MakerID);
            Console.Write("Please enter new person(id) that will make the task: ");
            string newMakerId = Console.ReadLine();
            Console.WriteLine("Is task done?: {0}",task.IsDone);
            Console.Write("Please enter if task is done?(true or false): ");
            string newIsDone = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
                task.Name = newName;
            if (!string.IsNullOrEmpty(newDescription))
                task.Description = newDescription;
            if (!string.IsNullOrEmpty(newEstimatedTime))
                task.EstimatedTime = Int32.Parse(newEstimatedTime);
            if (!string.IsNullOrEmpty(newMakerId))
                task.MakerID = Int32.Parse(newMakerId);
            if (!string.IsNullOrEmpty(newIsDone))
                task.IsDone = Convert.ToBoolean(newIsDone);
            task.DateLastUpdated = DateTime.Now;
            taskRepo.Save(task);
            Console.WriteLine("Task updated successfully!");
            Console.ReadKey(true);
        }

        

        private UserViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User view:");
                Console.WriteLine("My [t]asks");
                Console.WriteLine("[C]reate a task");
                Console.WriteLine("[D]elete a task");
                Console.WriteLine("[U]pdate a task");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "T":
                        {
                            return UserViewEnum.MyTasks; 
                        }
                    case "C":
                        {
                            return UserViewEnum.Add;
                        }
                    case "D":
                        {
                            return UserViewEnum.Delete;
                        }
                    case "U":
                        {
                            return UserViewEnum.Update;
                        }
                    case "X":
                        {
                            return UserViewEnum.Exit;
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
    }
}
