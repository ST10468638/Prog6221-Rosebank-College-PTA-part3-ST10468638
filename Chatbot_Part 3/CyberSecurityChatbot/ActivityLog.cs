using System;

namespace CyberSecurityChatbot
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public string ActivityDescription { get; set; }

        public DateTime ActivityDate { get; set; }

        public ActivityLog()
        {
            ActivityDescription = "";
            ActivityDate = DateTime.Now;
        }
    }
}