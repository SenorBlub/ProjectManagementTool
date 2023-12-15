using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models;

public class Employee : IEmployee
{
    public Guid guid { get; set; }
    public string name { get; set; }

    public string email { get; set; }
}