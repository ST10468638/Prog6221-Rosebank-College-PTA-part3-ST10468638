using System;

namespace CyberSecurityChatbot
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public DateTime ReminderDate { get; set; }

        public bool IsCompleted { get; set; }

        public TaskItem()
        {
            TaskName = "";
            ReminderDate = DateTime.Now;
            IsCompleted = false;
        }
    }
}