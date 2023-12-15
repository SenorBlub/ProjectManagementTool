namespace ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;

public interface IEmployeeProjectRepository
{
    void PostEmployeeProject(Guid employee, Guid project);
    void DeleteEmployeeProject(Guid employeeGuid, Guid projectGuid);
}