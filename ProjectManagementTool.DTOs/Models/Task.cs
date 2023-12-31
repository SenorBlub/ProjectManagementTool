﻿using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models;

public class Task : ITask
{
    public Guid guid { get; set; }

    public string description { get; set; }

    public string title { get; set; }

    public DateTime deadline { get; set; }

    public bool isNew { get; set; }
}