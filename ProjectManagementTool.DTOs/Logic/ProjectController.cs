using ProjectManagementTool.Logic.Interfaces.IRepositories;
using ProjectManagementTool.Models;
using Task = ProjectManagementTool.Models.Task;

namespace ProjectManagementTool.Logic.Logic;

public class ProjectController
{
    private string nullTitle { get; set; } = string.Empty;
    private string nullDescription { get; set; } = string.Empty;
    private Guid nullGuid { get; set; } = Guid.Empty;
    private List<Task> nullTasks { get; set; } = new List<Task>()
    {

    };
    private Goal nullGoal { get; set; } = new Goal()
    {

    };

    private List<Employee> nullEmployees { get; set; } = new List<Employee>()
    {

    };
    private bool nullIsNew { get; set; } = false;
    public void AddProject(string projectTitle = null, string projectDescription = null, Guid guid = default, List<Task> tasks = null, Goal goal = null, List<Employee> employees = null, bool isNew = false)
    {
        projectTitle ??= nullTitle;
        projectDescription ??= nullDescription;
        guid = guid == default ? nullGuid : guid;
        tasks ??= nullTasks;
        goal ??= nullGoal;
        employees ??= nullEmployees;
        isNew = isNew || nullIsNew;
    }

    public void AddProject(Project project)
    {

    }
}