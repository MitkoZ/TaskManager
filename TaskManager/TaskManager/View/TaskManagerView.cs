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
                    case TaskManagerViewEnum.ShouldMakeTasks:
                        {
                            ViewShouldMake();
                            break;
                        }
                    case TaskManagerViewEnum.CountTime:
                        {
                            CountTime();
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
            ViewShouldMake();
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
                            return TaskManagerViewEnum.ShouldMakeTasks;
                        }
                    case "T":
                        {
                            return TaskManagerViewEnum.CountTime;
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
        private void ViewShouldMake() //the tasks that the user has to do
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> shouldMakeTasks = taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id);
            if (shouldMakeTasks.Capacity == 0)
            {
                Console.WriteLine("You don't have any tasks to do");
                Console.ReadKey(true);
                return;
            }
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
                Console.WriteLine("####################################");
            }
            Console.ReadKey(true);
        }

        private void ViewCreated() //the tasks that the user has created
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> createdTasks = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            if (createdTasks.Capacity == 0)
            {
                Console.WriteLine("You don't have any created tasks");
                Console.ReadKey(true);
                return;
            }
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
                Console.WriteLine("####################################");
            }
            Console.ReadKey(true);
        }

        public void ChangeStatusCreated() //changes the status of a task that has been created by the user
        {
            Console.Clear();
            ViewCreated();
            Console.Write("Please enter the id of the task that you would want to change the status of: ");
            int idInput=Int32.Parse(Console.ReadLine());
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> createdTasks = taskRepo.GetAll(AuthenticationService.LoggedUser.Id);
            for (int i = 0; i < createdTasks.Count; i++)
            {
                if (createdTasks[i].Id == idInput)
                {
                    createdTasks[i].IsDone = false;
                    Console.WriteLine("Please write a comment:");
                    Comment();//todo
                }
            }
        }
        
        private void Comment()//todo
        {
            Console.ReadLine();
        }
    }
}
