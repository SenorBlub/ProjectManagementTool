namespace ProjectManagementTool.Logic.Interfaces.IModels;

public interface IGoal
{
    public Guid guid { get; set; }
    public List<ITask> tasks { get; set; }

    public int completionPercentage { get; set; }

    public Guid projectGuid { get; set; }
}