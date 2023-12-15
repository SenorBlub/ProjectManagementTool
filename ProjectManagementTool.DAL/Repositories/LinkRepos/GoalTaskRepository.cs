using MySqlConnector;
using ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories
{
    public class GoalTaskRepository : IGoalTaskRepository
    {
        readonly MySqlConnection _connection;

        public GoalTaskRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        public void PostGoalTask(Guid goal, Guid task)
        {
            _connection.Open();
            string query = "INSERT INTO GoalTask (goalGuid, taskGuid) VALUES (@goalGuid, @taskGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@goalGuid", goal);
            command.Parameters.AddWithValue("@taskGuid", task);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteGoalTask(Guid goalGuid, Guid taskGuid)
        {
            _connection.Open();
            string query = "DELETE FROM GoalTask WHERE goalGuid = @goalGuid AND taskGuid = @taskGuid";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@goalGuid", goalGuid);
            command.Parameters.AddWithValue("@taskGuid", taskGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}