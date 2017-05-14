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
    class CommentManagementView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                CommentManagementViewEnum choice = RenderMenu();
                switch (choice)
                {
                    case CommentManagementViewEnum.MakeComment:
                        {
                            MakeComment();
                            break;
                        }
                    case CommentManagementViewEnum.ViewComments:
                        {
                            ViewComments();
                            break;
                        }
                    case CommentManagementViewEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }

        private CommentManagementViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Make a C[o]mment");
                Console.WriteLine("Vi[e]w a Comment");
                Console.WriteLine("E[x]it");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "O":
                        {
                            return CommentManagementViewEnum.MakeComment;
                        }
                    case "E":
                        {
                            return CommentManagementViewEnum.ViewComments;
                        }
                    case "X":
                        {
                            return CommentManagementViewEnum.Exit;
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

        public void MakeComment()
        {
            Console.Clear();
            TaskRepository taskRepo = new TaskRepository("tasks.txt");
            List<TaskEntity> tasksList = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);//tasks created by me
            tasksList.AddRange(taskRepo.GetAllToMake(AuthenticationService.LoggedUser.Id));
            foreach (TaskEntity task in tasksList)
            {
                Console.WriteLine("Task id: " + task.Id);
                Console.WriteLine("Task name: " + task.Name);
                Console.WriteLine("#####################################");
            }
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
            for (int i = 0; i < tasksList.Count; i++)
            {
                if (tasksList[i].Id == idInput)
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
            List<TaskEntity> myTasksList = taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id*/u=>u.ParentUserId==AuthenticationService.LoggedUser.Id);
            myTasksList.AddRange(taskRepo.GetAll(/*AuthenticationService.LoggedUser.Id)*/ u=>u.AssigneeID==AuthenticationService.LoggedUser.Id));
            CommentRepository commentRepo = new CommentRepository("comments.txt");
            List<CommentEntity> allCommentsList = commentRepo.GetAll();
            if (myTasksList.Count != 0)
            {
                foreach (TaskEntity task in myTasksList)
                {
                    Console.WriteLine("Task id: " + task.Id);
                    Console.WriteLine("Task name: " + task.Name);
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
            bool idExists = false;
            for (int i = 0; i < myTasksList.Count; i++)
            {
                if (myTasksList[i].Id == idInput)
                {
                    idExists = true;
                    break;
                }
            }
            if (idExists == false)
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
                if (commentEntity.TaskId == idInput)
                {
                    currentTaskComments.Add(new CommentEntity { ParentUserId = commentEntity.ParentUserId, Comment = commentEntity.Comment });
                }
            }
            if (currentTaskComments.Count == 0)
            {
                Console.WriteLine("This tasks doesn't have any comments");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            UsersRepository userRepo = new UsersRepository("users.txt");
            foreach (CommentEntity comment in currentTaskComments)
            {

                Console.WriteLine("Commenter: " + userRepo.GetById(comment.ParentUserId).Username);
                Console.WriteLine("Comment: " + comment.Comment);
            }
            Console.ReadKey(true);
            fileStream.Close();
            streamReader.Close();
        }
    }
}
