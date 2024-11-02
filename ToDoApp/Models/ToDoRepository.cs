using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ToDoApp.Models
{
    public class ToDoRepository
    {
        private readonly string _connectionString;

        public ToDoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ToDoItem> GetAll(string sort = null, string status = null, string jobToDo = null)
        {
            var todos = new List<ToDoItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryBuilder = new StringBuilder("SELECT * FROM ToDo WHERE 1=1");

                // Filter by Status if specified
                if (!string.IsNullOrEmpty(status))
                {
                    queryBuilder.Append(" AND Status = @Status");
                }

                // Search by JobToDo if specified
                if (!string.IsNullOrEmpty(jobToDo))
                {
                    queryBuilder.Append(" AND JobToDo LIKE @JobToDo");
                }

                // Sort by StartDate if specified
                if (sort == "asc")
                {
                    queryBuilder.Append(" ORDER BY StartDate ASC");
                }
                else if (sort == "desc")
                {
                    queryBuilder.Append(" ORDER BY StartDate DESC");
                }

                using (var command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    if (!string.IsNullOrEmpty(status))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                    }

                    if (!string.IsNullOrEmpty(jobToDo))
                    {
                        command.Parameters.AddWithValue("@JobToDo", "%" + jobToDo + "%");
                    }

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todos.Add(new ToDoItem
                            {
                                Id = (int)reader["Id"],
                                JobToDo = reader["JobToDo"].ToString(),
                                Status = reader["Status"].ToString(),
                                StartDate = (DateTime)reader["StartDate"]
                            });
                        }
                    }
                }
            }
            return todos;
        }

        public ToDoItem GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ToDo WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ToDoItem
                        {
                            Id = (int)reader["Id"],
                            JobToDo = reader["JobToDo"].ToString(),
                            Status = reader["Status"].ToString(),
                            StartDate = (DateTime)reader["StartDate"]
                        };
                    }
                }
            }
            return null;
        }

        public void Add(ToDoItem todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (@JobToDo, @Status, @StartDate)", connection);
                command.Parameters.AddWithValue("@JobToDo", todo.JobToDo);
                command.Parameters.AddWithValue("@Status", todo.Status);
                command.Parameters.AddWithValue("@StartDate", todo.StartDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(ToDoItem todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE ToDo SET JobToDo = @JobToDo, Status = @Status, StartDate = @StartDate WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", todo.Id);
                command.Parameters.AddWithValue("@JobToDo", todo.JobToDo);
                command.Parameters.AddWithValue("@Status", todo.Status);
                command.Parameters.AddWithValue("@StartDate", todo.StartDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM ToDo WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStatus(ToDoItem todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE ToDo SET Status = @Status WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", todo.Id);
                command.Parameters.AddWithValue("@Status", todo.Status);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
