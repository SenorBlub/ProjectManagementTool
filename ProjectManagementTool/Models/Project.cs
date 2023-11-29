namespace ProjectManagementTool.Models;

public class Project
{
    public Guid guid { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public List<Task> tasks { get; set; }

    public Goal goal { get; set; }

    public List<Employee> assignees { get; set; }

    public bool isNew { get; set; }
}