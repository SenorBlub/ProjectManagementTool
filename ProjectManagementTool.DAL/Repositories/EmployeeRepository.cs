using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories;

public class EmployeeRepository
{
    readonly MySqlConnection _connection;

    public EmployeeRepository()
    {
        _connection =
            new MySqlConnection(
                "Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }
    // GetEmployee to get a single employee by ID
    public Models.Employee GetEmployee(Guid id)
    {
        Models.Employee employee = new Models.Employee();
        _connection.Open();
        string query = "SELECT * FROM Employee WHERE guid = @id";
        using MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            employee.email = reader["email"].ToString();
            employee.name = reader["name"].ToString();
            employee.guid = (Guid)reader["guid"];
        }
        _connection.Close();
        return employee;
    }
    // GetEmployees to get all employees
    public List<Models.Employee> GetEmployees()
    {
        List<Models.Employee> employees = new List<Models.Employee>();
        _connection.Open();
        string query = "SELECT * FROM Employee";
        using MySqlCommand command = new MySqlCommand(query, _connection);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            employees.Add(new Employee{
            email = reader["email"].ToString(),
            name = reader["name"].ToString(),
            guid = (Guid)reader["guid"]
            });
        }
        _connection.Close();
        return employees;
    }

    // PostEmployee to add an employee


    // UpdateEmployee to edit a single employee by ID
    // DeleteEmployee to delete a single employee by ID
}