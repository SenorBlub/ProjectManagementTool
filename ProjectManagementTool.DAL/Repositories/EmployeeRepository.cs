using MySqlConnector;
using ProjectManagementTool.Logic.Interfaces.IRepositories.ILinkRepositories;
using ProjectManagementTool.Logic.Interfaces.IModels;
using ProjectManagementTool.Logic.Interfaces.IRepositories;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    readonly MySqlConnection _connection;

    public EmployeeRepository()
    {
        _connection =
            new MySqlConnection(
                "Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }
    // GetEmployee to get a single employee by ID
    public IEmployee GetEmployee(Guid employeeId)
    {
        IEmployee employee = new Employee();
        _connection.Open();
        string query = "SELECT * FROM Employee WHERE guid = @employeeId";
        using MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@employeeId", employeeId);

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
    public List<IEmployee> GetEmployees()
    {
        List<IEmployee> employees = new List<IEmployee>();
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

    // GetEmployees to get all employees
    public List<IEmployee> GetProjectEmployees(Guid employeeId)
    {
        List<IEmployee> employees = new List<IEmployee>();
        _connection.Open();
        string query = "SELECT Employee.* FROM EmployeeProject RIGHT JOIN Employee ON EmployeeProject.employeeGuid = Employee.guid WHERE EmployeeProject.projectGuid = @employeeId"; // !TODO Clarity of definition
        using MySqlCommand command = new MySqlCommand(query, _connection); // !TODO only fetch relevant data (performance)
        command.Parameters.AddWithValue("@employeeId", employeeId);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            employees.Add(new Employee
            {
                email = reader["email"].ToString(),
                name = reader["name"].ToString(),
                guid = (Guid)reader["employeeGuid"]
            });
        }
        _connection.Close();
        return employees;
    }

    // PostEmployee to add an employee
    public void PostEmployee(IEmployee employee)
    {
        _connection.Open();
        string query = "INSERT INTO Employee (guid, name, email) VALUES (@employeeId, @name, @email)";
        using MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@employeeId", employee.guid);
        command.Parameters.AddWithValue("@name", employee.name);
        command.Parameters.AddWithValue("@email", employee.email);

        command.ExecuteNonQuery();
        _connection.Close();
    }

    // UpdateEmployee to edit a single employee by ID
    public void UpdateEmployee(IEmployee employee, Guid employeeId)
    {
        _connection.Open();
        string query = "Update Employee SET guid = @employeeId, name =  @name, email = @email";
        using MySqlCommand command = new MySqlCommand(query, _connection);

        command.Parameters.AddWithValue("@employeeId", employeeId);
        command.Parameters.AddWithValue("@name", employee.name);
        command.Parameters.AddWithValue("@email", employee.email);

        command.ExecuteNonQuery();
        _connection.Close();
    }

    // DeleteEmployee to delete a single employee by ID
    public void DeleteEmployee(Guid employeeId)
    {
        _connection.Open();
        string query = "DELETE FROM Employee WHERE guid = @employeeId";
        var command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@employeeId", employeeId);

        command.ExecuteNonQuery();
        _connection.Close();
    }
}