using System;
using System.Data;
using System.Text;

namespace CyberSecurityChatbot
{
    public class TaskManager
    {
        private DatabaseManager databaseManager;

        public TaskManager()
        {
            databaseManager = new DatabaseManager();
        }

        public void AddTask(
            string taskName,
            string description,
            DateTime reminderDate)
        {
            databaseManager.AddTask(
                taskName,
                description,
                reminderDate,
                false);
        }

        public void CompleteTask(int id)
        {
            databaseManager.CompleteTask(id);
        }

        public void DeleteTask(int id)
        {
            databaseManager.DeleteTask(id);
        }

        public string DisplayTasks()
        {
            DataTable table =
                databaseManager.GetTasks();

            if (table.Rows.Count == 0)
            {
                return "No tasks available.";
            }

            StringBuilder result =
                new StringBuilder();

            foreach (DataRow row in table.Rows)
            {
                result.AppendLine(
                    $"ID: {row["Id"]}");

                result.AppendLine(
                    $"Task: {row["TaskName"]}");

                result.AppendLine(
                    $"Description: {row["Description"]}");

                result.AppendLine(
                    $"Reminder: {row["ReminderDate"]}");

                result.AppendLine(
                    $"Completed: {row["IsCompleted"]}");

                result.AppendLine("---------------------");
            }

            return result.ToString();
        }
    }
}