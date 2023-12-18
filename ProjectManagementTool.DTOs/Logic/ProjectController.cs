using ProjectManagementTool.Logic.Interfaces.IRepositories;
using ProjectManagementTool.Models;
using System;
using System.Collections.Generic;
using ProjectManagementTool.Logic.Interfaces.IModels;
using Task = ProjectManagementTool.Models.Task;

namespace ProjectManagementTool.Logic.Logic
{
    public class ProjectController
    {
        private readonly string NullTitle = string.Empty;
        private readonly string NullDescription = string.Empty;
        private readonly Guid NullGuid = Guid.Empty;
        private readonly List<ITask> NullTasks = new List<ITask>();
        private readonly IGoal NullGoal = new Goal();
        private readonly List<IEmployee> NullEmployees = new List<IEmployee>();
        private readonly bool NullIsNew = false;

        public void AddProject(IProjectRepository repository, bool isNew = false, string projectTitle = null,
            string projectDescription = null, Guid guid = default, List<ITask> tasks = null, IGoal goal = null,
            List<IEmployee> employees = null)
        {
            projectTitle ??= NullTitle;
            projectDescription ??= NullDescription;
            guid = guid == default ? NullGuid : guid;
            tasks ??= NullTasks;
            goal ??= NullGoal;
            employees ??= NullEmployees;
            isNew = isNew || NullIsNew;

            Project newProject = new Project
            {
                title = projectTitle,
                description = projectDescription,
                guid = guid,
                isNew = isNew,
                tasks = tasks,
                goal = goal,
                assignees = employees
            };

            repository.PostProject(newProject);
        }

        public void AddProject(IProject project, IProjectRepository repository)
        {

            repository.PostProject(project);

        }

        public void UpdateProject(IProjectRepository repository, Guid guid, bool isNew = false, string projectTitle = null,
            string projectDescription = null, List<ITask> tasks = null, IGoal goal = null,
            List<IEmployee> employees = null)
        {
            projectTitle ??= NullTitle;
            projectDescription ??= NullDescription;
            tasks ??= NullTasks;
            goal ??= NullGoal;
            employees ??= NullEmployees;
            isNew = isNew || NullIsNew;

            Project newProject = new Project
            {
                title = projectTitle,
                description = projectDescription,
                guid = guid,
                isNew = isNew,
                tasks = tasks,
                goal = goal,
                assignees = employees
            };

            repository.UpdateProject(newProject, newProject.guid);
        }

        public void UpdateProject(IProject project, IProjectRepository repository)
        {

            repository.UpdateProject(project, project.guid);

        }

        public void DeleteProject(Guid guid, IProjectRepository repository)
        {
            repository.DeleteProject(guid);
        }

        public List<IProject> GetProject(IProjectRepository repository)
        {
            

            return repository.GetProjects().ToList();
        }

        public IProject GetProject(Guid guid, IProjectRepository repository)
        {
            return repository.GetProject(guid);
        }

        public List<IProject> GetEmployeeProjects(Guid employeeGuid, IProjectRepository repository)
        {
            return repository.GetEmployeeProjects(employeeGuid).ToList();
        }
    }
}
