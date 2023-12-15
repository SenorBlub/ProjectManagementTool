namespace ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;

public interface IGoalTaskRepository
{
    void PostGoalTask(Guid goal, Guid task);
    void DeleteGoalTask(Guid goalGuid, Guid taskGuid);
}