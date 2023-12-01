namespace ProjectManagementTool.Models.NonBasicModels;

public class ProjectCreationModel
{
    public Project project { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
    public List<Guid> employees = new List<Guid>();
}