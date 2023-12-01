using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementTool.Models.NonBasicModels;
using System;
using ProjectManagementTool.DAL.Repositories;
using ProjectManagementTool.DAL.Repositories.LinkRepos;
using ProjectManagementTool.Models;
using Task = ProjectManagementTool.Models.Task;

namespace ProjectManagementTool.Pages
{
    public class ProjectCreatorModel : PageModel
    {
        [BindProperty]
        public ProjectCreationModel projectCreationModel { get; set; } = new ProjectCreationModel();

        public IActionResult OnPostCreateTask()
        {
            projectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();
            // Retrieve form values
            var taskTitle = Request.Form["TaskTitle"];
            var taskDescription = Request.Form["TaskDescription"];
            var taskDeadline = Request.Form["TaskDeadline"];

            // Validate and create new task
            if (!string.IsNullOrWhiteSpace(taskTitle) && DateTime.TryParse(taskDeadline, out var deadline))
            {
                var newTask = new Models.Task
                {
                    title = taskTitle,
                    description = taskDescription,
                    deadline = deadline,
                    guid = Guid.NewGuid(),
                    isNew = true
                };

                // Add the task to ProjectCreationModel.Tasks
                projectCreationModel.Tasks.Add(newTask);

                // Save ProjectCreationModel to session
                HttpContext.Session.SetObject("ProjectCreationModel", projectCreationModel);
            }
            else
            {
                // Handle invalid input, if needed
            }

            // Redirect back to the page
            return RedirectToPage();
        }

        public IActionResult OnPostAddAssignee()
        {
            projectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            // Retrieve form values
            var employeeIdString = Request.Form["AssigneeID"];

            if (Guid.TryParse(employeeIdString, out Guid employeeId))
            {
                projectCreationModel.employees.Add(employeeId);

                HttpContext.Session.SetObject("ProjectCreationModel", projectCreationModel);

                // Add a debug statement to check the count after adding
                Console.WriteLine($"Number of employees after adding: {projectCreationModel.employees.Count}");

                return RedirectToPage();
            }
            else
            {
                return BadRequest("Invalid employee ID");
            }
        }

        public IActionResult OnPostProjectOnly()
        {
            projectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();
            // Retrieve form values
            string projectTitle = Request.Form["ProjectTitle"];
            string projectDescription = Request.Form["ProjectDescription"];

            // Validate and create project
            if (!string.IsNullOrWhiteSpace(projectTitle))
            {
                Project newProject = new Project
                {
                    title = projectTitle,
                    description = projectDescription,
                    guid = Guid.NewGuid(),
                    isNew = true,
                    goal = new Goal
                    {
                        completionPercentage = 0,
                        guid = Guid.Empty,
                        projectGuid = Guid.Empty
                    }

                };

                projectCreationModel.project = newProject;

                TaskRepository taskRepository = new TaskRepository();
                foreach (Task task in projectCreationModel.Tasks)
                {
                    taskRepository.PostTask(task);
                }
                
                ProjectRepository projectRepository = new ProjectRepository();
                projectRepository.PostProject(projectCreationModel.project);

                EmployeeProjectRepository employeeProjectRepository = new EmployeeProjectRepository();
                foreach (Guid employee in projectCreationModel.employees)
                {
                    employeeProjectRepository.PostEmployeeProject(employee, projectCreationModel.project.guid);
                }

                TaskProjectRepository taskProjectRepository = new TaskProjectRepository();
                foreach (Task task in projectCreationModel.Tasks)
                {
                    taskProjectRepository.PostTaskProject(task.guid, projectCreationModel.project.guid);
                }

                // Save ProjectCreationModel to session or database, depending on your needs
                HttpContext.Session.SetObject("ProjectCreationModel", projectCreationModel);

                // Redirect back to the page or another page as needed
                return RedirectToPage();
            }
            else
            {
                // Handle invalid input for project, if needed
                return BadRequest("Invalid project input");
            }
        }

        public void OnGet()
        {
            projectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            Console.WriteLine($"Number of employees: {projectCreationModel.employees.Count}");

        }
    }
}
