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
            UserViewEnum choice = RenderMenu();
            while (true)
            {
                switch (choice)
                {
                    case UserViewEnum.Add:
                        {
                            Add();
                            break;
                        }
                    case UserViewEnum.View:
                        {
                            View();
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
            Console.WriteLine("Delete task: ");
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


        private void View()
        {
            Console.Clear();
            Console.Write("Please enter task id: ");//todo
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
         
        }

        

        private UserViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User view");
                Console.WriteLine("[C]reate a task");
                Console.WriteLine("[D]elete a task");
                Console.WriteLine("[U]pdate a task");
                Console.WriteLine("[V]iew a task");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
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
                    case "V":
                        {
                            return UserViewEnum.View;
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
