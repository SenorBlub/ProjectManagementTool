using MySqlConnector;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories;

public class ProjectRepository
{
    private readonly MySqlConnection _connection;

    public ProjectRepository()
    {
        _connection =
            new MySqlConnection(
                "Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }
    // GetProject to display project by ID
    public Project GetProject(Guid id)
    {
        Project project = new Project();
        _connection.Open();
        string query = "SELECT * FROM Project WHERE ID = @id";
        using MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            project.guid = (Guid)reader["guid"];
            project.description = reader["description"].ToString();
            project.title = reader["title"].ToString();
            project.isNew = (bool)reader["isNew"];
            project.goal = (new GoalRepository().GetGoal((Guid)reader["guid"]));
            project.tasks = (new TaskRepository()).GetProjectTasks((Guid)reader["guid"]).ToList();
            project.assignees = (new EmployeeRepository()).GetProjectEmployees((Guid)reader["guid"]).ToList();
        }

        _connection.Close();
        return project;
    }

    // GetProjects to display all projects
    public IEnumerable<Project> GetProjects()
    {
        List<Project> projects = new List<Project>();
        _connection.Open();
        string query = "SELECT * FROM Project";
        using MySqlCommand command = new MySqlCommand(query, _connection);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            projects.Add(
                new Project
                     {
                    guid = (Guid)reader["guid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    isNew = (bool)reader["isNew"],
                    goal = (new GoalRepository().GetGoal((Guid)reader["guid"])),
                    tasks = (new TaskRepository()).GetProjectTasks((Guid)reader["guid"]).ToList(),
                    assignees = (new EmployeeRepository()).GetProjectEmployees((Guid)reader["guid"]).ToList()
                     });
        }

        _connection.Close();
        return projects;
    }

    // GetEmployeeProjects to get all projects associated with a single employee
    public IEnumerable<Project> GetEmployeeProjects(Guid id)
    {
        List<Project> projects = new List<Project>();
        _connection.Open();
        string query = "SELECT * FROM EmployeeProject RIGHT JOIN Project ON EmployeeProject.projectGuid = Project.guid WHERE employeeGuid = @id";
        using MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);
        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            projects.Add(
                new Project
                {
                    guid = (Guid)reader["guid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    isNew = (bool)reader["isNew"],
                    goal = (new GoalRepository().GetGoal((Guid)reader["guid"])),
                    tasks = (new TaskRepository()).GetProjectTasks((Guid)reader["guid"]).ToList(),
                    assignees = (new EmployeeRepository()).GetProjectEmployees((Guid)reader["guid"]).ToList()
                });
        }

        _connection.Close();
        return projects;
    }

    // PostProject to create a project
    public void PostProject(Project project)
    {
        _connection.Open();

        string query = "INSERT INTO Project (guid, title, description, goalGuid, isNew) VALUES (@id, @title, @description, @goalGuid, @isNew)";
        MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@id", project.guid);
        command.Parameters.AddWithValue("@title", project.title);
        command.Parameters.AddWithValue("@description", project.description);
        command.Parameters.AddWithValue("@goalGuid", project.goal.guid);
        command.Parameters.AddWithValue("@isNew", project.isNew);

        command.ExecuteNonQuery();

        foreach (Models.Task task in project.tasks)
        {
            MySqlCommand taskCommand = new MySqlCommand("INSERT INTO TaskProject (projectGuid, taskGuid) VALUES (@projectId, @taskId)",
                _connection);
            taskCommand.Parameters.AddWithValue("@projectId", project.guid);
            taskCommand.Parameters.AddWithValue("@taskId", task.guid);
            taskCommand.ExecuteNonQuery();
        }

        foreach (Models.Employee employee in project.assignees)
        {
            MySqlCommand taskCommand = new MySqlCommand("INSERT INTO EmployeeProject (projectGuid, employeeGuid) VALUES (@projectId, @employeeId)",
                _connection);
            taskCommand.Parameters.AddWithValue("@projectId", project.guid);
            taskCommand.Parameters.AddWithValue("@employeeId", employee.guid);
            taskCommand.ExecuteNonQuery();
        }

        _connection.Close();
    }

    // UpdateProject to change any values associated with a project
    public void UpdateProject(Project project, Guid id)
    {
        _connection.Open();

        string query = "UPDATE Project ON title = @title, description = @description, goalGuid = @goalGuid, isNew = @isNew) Where guid = @id";
        MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@id", project.guid);
        command.Parameters.AddWithValue("@title", project.title);
        command.Parameters.AddWithValue("@description", project.description);
        command.Parameters.AddWithValue("@goalGuid", project.goal.guid);
        command.Parameters.AddWithValue("@isNew", project.isNew);

        command.ExecuteNonQuery();

        foreach (Models.Task task in project.tasks)
        {
            MySqlCommand taskCommand = new MySqlCommand("UPDATE TaskProject ON taskGuid = @taskId WHERE projectGuid = @projectId",
                _connection);
            taskCommand.Parameters.AddWithValue("@projectId", project.guid);
            taskCommand.Parameters.AddWithValue("@taskId", task.guid);
            taskCommand.ExecuteNonQuery();
        }

        foreach (Models.Employee employee in project.assignees)
        {
            MySqlCommand taskCommand = new MySqlCommand("UPDATE EmployeeProject ON employeeGuid = @employeeId WHERE projectGuid = @projectId",
                _connection);
            taskCommand.Parameters.AddWithValue("@projectId", project.guid);
            taskCommand.Parameters.AddWithValue("@employeeId", employee.guid);
            taskCommand.ExecuteNonQuery();
        }

        _connection.Close();
    }

    // DeleteProject to remove a project by ID
    public void DeleteProject(Guid id)
    {
        _connection.Open();
            string query = "DELETE FROM Project WHERE guid = @id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            string query2 = "DELETE FROM EmployeeProject WHERE guid = @id";
            var command2 = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            string query3 = "DELETE FROM TaskProject WHERE guid = @id";
            var command3 = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        _connection.Close();
        
    }
}   