using MySqlConnector;
using ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories
{
    public class TaskProjectRepository : ITaskProjectRepository
    {
        readonly MySqlConnection _connection;

        public TaskProjectRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        public void PostTaskProject(Guid task, Guid project)
        {
            _connection.Open();
            string query = "INSERT INTO TaskProject (taskGuid, projectGuid) VALUES (@taskGuid, @projectGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@taskGuid", task);
            command.Parameters.AddWithValue("@projectGuid", project);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteTaskProject(Guid taskGuid, Guid projectGuid)
        {
            _connection.Open();
            string query = "DELETE FROM TaskProject WHERE taskGuid = @taskGuid AND projectGuid = @projectGuid";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskGuid", taskGuid);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}