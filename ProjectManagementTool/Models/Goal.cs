namespace ProjectManagementTool.Models;

public class Goal
{
    public Guid guid { get; set; }
    public List<Task> tasks { get; set; }

    public int completionPercentage { get; set; }

    public Guid projectGuid { get; set; }
}