using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace CyberSecurityChatbot
{
    public class DatabaseManager
    {
        private string connectionString =
            "server=localhost;database=cybersecuritychatbotdb;uid=root;pwd=Terror@0307;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void AddTask(string taskName,
                            string description,
                            DateTime reminderDate,
                            bool isCompleted)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO Tasks
                                    (TaskName, Description, ReminderDate, IsCompleted)
                                    VALUES
                                    (@TaskName, @Description, @ReminderDate, @IsCompleted)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TaskName", taskName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ReminderDate", reminderDate);
                    cmd.Parameters.AddWithValue("@IsCompleted", isCompleted);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AddTask Error: " + ex.Message);
            }
        }

        public DataTable GetTasks()
        {
            DataTable table = new DataTable();

            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = "SELECT * FROM Tasks";

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn);

                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetTasks Error: " + ex.Message);
            }

            return table;
        }

        public void DeleteTask(int id)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = "DELETE FROM Tasks WHERE Id=@Id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DeleteTask Error: " + ex.Message);
            }
        }

        public void CompleteTask(int id)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE Tasks
                                     SET IsCompleted = 1
                                     WHERE Id=@Id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CompleteTask Error: " + ex.Message);
            }
        }
    }
}