using ProjectManagementTool.Logic.Interfaces.IModels;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Logic.Interfaces.IRepositories;

public interface IProjectRepository
{
    IProject GetProject(Guid id);
    IEnumerable<IProject> GetProjects();
    IEnumerable<IProject> GetEmployeeProjects(Guid id);
    void PostProject(IProject project);
    void UpdateProject(IProject project, Guid id);
    void DeleteProject(Guid id);
}