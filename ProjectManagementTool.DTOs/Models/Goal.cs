﻿using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models;

public class Goal : IGoal
{
    public Guid guid { get; set; }
    public List<ITask> tasks { get; set; }

    public int completionPercentage { get; set; }

    public Guid projectGuid { get; set; }
}