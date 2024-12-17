using System.Data.SqlClient;
using System.Data.SqlTypes;
using Task.Models;

namespace Task.UnitOfWork
{
    public class UserUoF
    {
        private readonly string _connectionString;

        //public UserUoF()
        //{
        //}

        // Constructor that accepts IConfiguration for connection string
        public UserUoF(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to get the list of users from the database
        public List<User> GetUserList()
        {
            List<User> users = new List<User>();

            try
            {
                // Using the connection string to connect to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Define the SQL query to get all users
                    string query = "SELECT UserId, RoleId, FirstName, LastName, Email, Password FROM [User]";

                    // Execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the data and populate the list of users
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    UserID = reader.GetInt32(0), 
                                    RoleID = reader.GetInt32(1),
                                    FirstName = reader.GetString(2), 
                                    LastName = reader.GetString(3), 
                                    Email = reader.GetString(4), 
                                    Password = reader.GetString(5) 
                                };

                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            return users;
        }
    }
}

