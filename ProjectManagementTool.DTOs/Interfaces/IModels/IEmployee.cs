namespace ProjectManagementTool.Logic.Interfaces.IModels;

public interface IEmployee
{
    public Guid guid { get; set; }
    public string name { get; set; }

    public string email { get; set; }
}