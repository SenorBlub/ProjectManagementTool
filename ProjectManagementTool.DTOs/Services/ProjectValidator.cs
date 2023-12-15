using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Logic.Services;

public static class ProjectValidator
{
    public static Project Validate(Project project) //TODO make this function a bool as that makes more sense with that name, alternatively change the name
    {
        bool valid = true;
        if (project.assignees.Count == null || project.assignees.Count == 0)
        {
            valid = false;
            project.description += "No Assignees Specified. ";
            project.assignees.Add(new Employee
            {
                email = "admin@adminsEmailService.com",
                guid = Guid.Parse("81cc02a7-d5de-4365-8213-b67affa6f337"),
                name = "Admin The Almighty"
            });
        }

        if (project.tasks.Count == null || project.tasks.Count == 0)
        {
            valid = false;
            project.description += "No Tasks Specified. ";
        }

        if (project.goal == null)
        {
            Goal emptyGoal = new Goal
            {
                completionPercentage = 0,
                guid = Guid.Empty,
                projectGuid = project.guid,
                tasks = null
            };
            project.goal = emptyGoal;
        }

        if (project.description == null)
        {
                project.description += "Description Empty. ";
        }

        if (project.guid == null)
        {
            Goal emptyGoal = new Goal
            {
                completionPercentage = 0,
                guid = Guid.Empty,
                projectGuid = project.guid,
                tasks = null
            };
            project.guid = Guid.Empty;
            project.description = "This is an invalid project, thus you shouldnt be seeing this.";
            project.goal = emptyGoal;
            project.assignees = null;
            project.tasks = null;
            project.isNew = false;
            project.title = "The undefined: Special edition";
        }

        return project;
    }
}