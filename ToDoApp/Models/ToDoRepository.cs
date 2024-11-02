using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public List<ToDoItem> GetAll()
        {
            var todos = new List<ToDoItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM ToDo", connection);
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
            return todos;
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
