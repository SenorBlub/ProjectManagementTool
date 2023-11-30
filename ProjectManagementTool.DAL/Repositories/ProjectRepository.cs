using System.ComponentModel;
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
            // add goal, employees and tasks
        }

        _connection.Close();
        return project;
    }
    // GetProjects to display all projects
    // PostProject to create a project
    // UpdateProject to change any values associated with a project
    // DeleteProject to remove a project by ID
}   