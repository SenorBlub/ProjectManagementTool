namespace ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;

public interface ITaskProjectRepository
{
    void PostTaskProject(Guid task, Guid project);
    void DeleteTaskProject(Guid taskGuid, Guid projectGuid);
}