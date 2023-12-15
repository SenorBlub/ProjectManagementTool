namespace ProjectManagementTool.Logic.Interfaces.IModels;

public interface ITask
{
    public Guid guid { get; set; }

    public string description { get; set; }

    public string title { get; set; }

    public DateTime deadline { get; set; }

    public bool isNew { get; set; }
}
}