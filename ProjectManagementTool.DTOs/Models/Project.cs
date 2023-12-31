﻿using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models;

public class Project : IProject
{
    public Guid guid { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public List<ITask> tasks { get; set; }

    public IGoal goal { get; set; }

    public List<IEmployee> assignees { get; set; }

    public bool isNew { get; set; }
}