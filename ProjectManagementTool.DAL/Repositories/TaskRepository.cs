using MySqlConnector;
using System;
using System.Security.Cryptography.X509Certificates;
using ProjectManagementTool.Logic.Interfaces.IModels;
using ProjectManagementTool.Logic.Interfaces.IRepositories;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        readonly MySqlConnection _connection;

        public TaskRepository()
        {
            _connection =
                new MySqlConnection(
                    "Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        // GetTask to get a task by ID
        public ITask GetTask(Guid id)
        {
            ITask task = new Models.Task();
            _connection.Open();
            string query = "SELECT * FROM Task WHERE guid = @taskId";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", id);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                task = new Models.Task
                {
                    guid = (Guid)reader["guid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    deadline = (DateTime)reader["deadline"],
                    isNew = (bool)reader["isNew"]
                };
            }
            _connection.Close();
            return task;
        }

        // GetAllTasks to get all tasks
        public IEnumerable<ITask> GetTasks()
        {
            _connection.Open();
            string query = "SELECT * FROM Task";
            MySqlCommand command = new MySqlCommand(query, _connection);

            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Models.Task
                {
                    guid = (Guid)reader["guid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    deadline = (DateTime)reader["deadline"],
                    isNew = (bool)reader["isNew"]
                });
            }

            _connection.Close();
            return tasks;
        }

        // GetEmployeeTasks to get all tasks that are associated with a single employee by ID
        public IEnumerable<ITask> GetEmployeeTasks(Guid id)
        {
            _connection.Open();
            string query = "SELECT Task.* FROM Task JOIN TaskProject ON Task.guid = TaskProject.taskGuid JOIN Project ON TaskProject.projectGuid = Project.guid JOIN EmployeeProject ON Project.guid = EmployeeProject.projectGuid JOIN Employee ON EmployeeProject.employeeGuid = Employee.guid WHERE Employee.guid = @id;";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Models.Task
                {
                    guid = (Guid)reader["guid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    deadline = (DateTime)reader["deadline"],
                    isNew = (bool)reader["isNew"]
                });
            }

            _connection.Close();
            return tasks;
        }

        // GetProjectTasks to get all tasks that are associated with a single project by ID
        public IEnumerable<ITask> GetProjectTasks(Guid id)
        {
            _connection.Open();
            string query = "SELECT * FROM TaskProject RIGHT JOIN Task ON TaskProject.taskGuid = Task.Guid WHERE projectGuid = @id";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Models.Task
                {
                    guid = (Guid)reader["taskGuid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    deadline = (DateTime)reader["deadline"],
                    isNew = (bool)reader["isNew"]
                });
            }
            _connection.Close();
            return tasks; // Return null if the task with the specified ID is not found
        }

        // get all tasks associated with a certain goal
        public IEnumerable<ITask> GetGoalTasks(Guid id)
        {
            _connection.Open();
            string query = "SELECT * FROM GoalTask RIGHT JOIN Task ON GoalTask.taskGuid = Task.Guid WHERE goalGuid = @id";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Models.Task
                {
                    guid = (Guid)reader["taskGuid"],
                    description = reader["description"].ToString(),
                    title = reader["title"].ToString(),
                    deadline = (DateTime)reader["deadline"],
                    isNew = (bool)reader["isNew"]
                });
            }
            _connection.Close();
            return tasks;
        }

        // PostTask to add a task
        public void PostTask(ITask task)
        {
            _connection.Open();
            string query = "INSERT INTO Task (guid, description, title, deadline, isNew) VALUES (@id, @description, @title, @deadline, @isNew)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@id", task.guid);
            command.Parameters.AddWithValue("@description", task.description);
            command.Parameters.AddWithValue("@title", task.title);
            command.Parameters.AddWithValue("@deadline", task.deadline);
            command.Parameters.AddWithValue("@isNew", task.isNew);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        // UpdateTask to edit a single task by ID
        public void UpdateTask(ITask task, Guid id)
        {
            _connection.Open();
            string query =
                "UPDATE Task SET description = @description, title = @title, deadline = @deadline, isNew = @isNew WHERE guid = @id";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@description", task.description);
            command.Parameters.AddWithValue("@title", task.title);
            command.Parameters.AddWithValue("@deadline", task.deadline);
            command.Parameters.AddWithValue("@isNew", task.isNew);
            command.Parameters.AddWithValue("@id", task.guid);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        // DeleteTask to delete a task by ID
        public void DeleteTask(Guid id)
        {
            _connection.Open();
            string query = "DELETE FROM Task WHERE guid = @id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}