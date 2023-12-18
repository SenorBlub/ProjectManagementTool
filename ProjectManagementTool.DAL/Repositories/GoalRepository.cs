using System.Net;
using MySqlConnector;
using ProjectManagementTool.Logic.Interfaces.IModels;
using ProjectManagementTool.Logic.Interfaces.IRepositories;
using ProjectManagementTool.Models;
using Task = ProjectManagementTool.Models.Task;

namespace ProjectManagementTool.DAL.Repositories;

public class GoalRepository : IGoalRepository
{
    readonly MySqlConnection _connection;

    public GoalRepository()
    {
        _connection =
            new MySqlConnection(
                "Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }

    // GetGoal to get a single goal by ID
    public IGoal GetGoal(Guid goalId)
    {
        IGoal goal = new Models.Goal();
        _connection.Open();
        string query = "SELECT * FROM Goal WHERE guid = @goalId";
        using MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@goalId", goalId);

        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            goal.guid = (Guid)reader["guid"];
            goal.completionPercentage = (int)reader["completionPercentage"];
            goal.tasks = (new TaskRepository()).GetGoalTasks(goalId).ToList();
        }
        return goal;
    }

    // GetGoals to get all goals
    public IEnumerable<IGoal> GetGoals()
    {
        List<IGoal> goals = new List<IGoal>();
        _connection.Open();
        string query = "SELECT * FROM Goal";
        using MySqlCommand command = new MySqlCommand(query, _connection);

        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            goals.Add(new Goal{
            guid = (Guid)reader["guid"],
            completionPercentage = (int)reader["completionPercentage"],
            tasks = (new TaskRepository()).GetGoalTasks((Guid)reader["guid"]).ToList()
            });
        }
        return goals;
    }

    // PostGoal to add a goal
    public void PostGoal(IGoal goal)
    {
        _connection.Open();

        string query = "INSERT INTO Goal (guid, completionPercentage) VALUES (@goalId, @completionPercentage)";
        MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@goalId", goal.guid);
        command.Parameters.AddWithValue("@completionPercentage", goal.completionPercentage);

        command.ExecuteNonQuery();

        foreach (ITask task in goal.tasks)
        {
            MySqlCommand taskCommand = new MySqlCommand("INSERT INTO GoalTask (goalGuid, taskGuid) VALUES (@goalId, @taskId)",
                _connection);
            taskCommand.Parameters.AddWithValue("@goalId", goal.guid);
            taskCommand.Parameters.AddWithValue("@taskId", task.guid);
            taskCommand.ExecuteNonQuery();
        }
        
        _connection.Close();
    }
    // UpdateGoal to change a goal
    public void UpdateGoal(IGoal goal, Guid goalId)
    {
        _connection.Open();

        string query = "UPDATE Goal SET completionPercentage = @completionPercentage WHERE guid = @goalId";
        MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@goalId", goal.guid);
        command.Parameters.AddWithValue("@completionPercentage", goal.completionPercentage);

        command.ExecuteNonQuery();

        foreach (ITask task in goal.tasks)
        {
            MySqlCommand taskCommand = new MySqlCommand("UPDATE GoalTask SET taskGuid = @taskId WHERE goalGuid = @goalId",
                _connection);
            taskCommand.Parameters.AddWithValue("@goalId", goal.guid);
            taskCommand.Parameters.AddWithValue("@taskId", task.guid);
            taskCommand.ExecuteNonQuery();
        }

        _connection.Close();
    }

    // DeleteGoal to delete a single goal by ID
    public void DeleteGoal(Guid goalId)
    {
        _connection.Open();
        string query = "DELETE FROM Goal WHERE guid = @goalId";
        var command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@goalId", goalId);

        command.ExecuteNonQuery();

        MySqlCommand taskCommand = new MySqlCommand("DELETE FROM GoalTask WHERE goalGuid = @goalId",
            _connection);
        taskCommand.ExecuteNonQuery();

        _connection.Close();
    }
}