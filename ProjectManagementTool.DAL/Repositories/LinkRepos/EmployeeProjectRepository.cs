using MySqlConnector;
using ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories.LinkRepos
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        readonly MySqlConnection _connection;

        public EmployeeProjectRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        public void PostEmployeeProject(Guid employeeGuid, Guid projectGuid)
        {
            _connection.Open();
            string query = "INSERT INTO EmployeeProject (employeeGuid, projectGuid) VALUES (@employeeGuid, @projectGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@employeeGuid", employeeGuid);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteEmployeeProject(Guid employeeGuid, Guid projectGuid)
        {
            _connection.Open();
            string query = "DELETE FROM EmployeeProject WHERE employeeGuid = @employeeGuid AND projectGuid = @projectGuid";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@employeeGuid", employeeGuid);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}