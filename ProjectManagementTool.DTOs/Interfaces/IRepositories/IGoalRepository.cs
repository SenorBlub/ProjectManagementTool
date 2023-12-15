using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Logic.Interfaces.IRepositories;

public interface IGoalRepository
{
    IGoal GetGoal(Guid id);
    IEnumerable<IGoal> GetGoals();
    void PostGoal(IGoal goal);
    void UpdateGoal(IGoal goal, Guid id);
    void DeleteGoal(Guid id);
}