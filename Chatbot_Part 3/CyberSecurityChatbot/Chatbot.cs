using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public class Chatbot
    {
        private string userName = "";
        private string favouriteTopic = "";
        private string currentTopic = "";

        private Random random = new Random();

        private Dictionary<string, List<string>> keywordResponses;

        // ===== SYSTEMS =====
        private TaskManager taskManager = new TaskManager();
        private QuizManager quizManager = new QuizManager();
        private List<string> activityLog = new List<string>();

        public Chatbot()
        {
            keywordResponses = new Dictionary<string, List<string>>();

            keywordResponses["password"] = new List<string>
            {
                "Use strong passwords with letters, numbers, and symbols.",
                "Avoid using personal information in your passwords.",
                "Use different passwords for different accounts."
            };

            keywordResponses["phishing"] = new List<string>
            {
                "Never click suspicious links from unknown emails.",
                "Scammers often pretend to be trusted organisations.",
                "Always verify email addresses before responding."
            };

            keywordResponses["privacy"] = new List<string>
            {
                "Review your social media privacy settings regularly.",
                "Do not share sensitive information publicly online.",
                "Enable two-factor authentication for better privacy."
            };

            keywordResponses["scam"] = new List<string>
            {
                "Be careful of online offers that seem too good to be true.",
                "Scammers often create urgency to trick victims.",
                "Never share banking details with strangers online."
            };
        }

        public string GetResponse(string input)
        {
            string originalInput = input;
            input = input.ToLower();

            LogActivity("USER: " + input);

            // ================= USER NAME =================
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = originalInput;
                return $"Nice to meet you, {userName}! Ask me about cybersecurity or type 'help'.";
            }

            if (input.Contains("what is my name"))
                return $"Your name is {userName}.";

            // ================= ACTIVITY LOG =================
            if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                return GetActivityLog();
            }

            // ================= NLP TASK SYSTEM =================

            // ADD TASK (flexible NLP)
            if (input.Contains("add task") || input.Contains("remind me") || input.Contains("set reminder"))
            {
                string taskText = ExtractTaskText(originalInput);
                DateTime reminder = DateTime.Now.AddDays(1);

                taskManager.AddTask(taskText, "Cybersecurity task", reminder);

                LogActivity("TASK ADDED: " + taskText);

                return $"Task added: '{taskText}'. Reminder set for tomorrow.";
            }

            if (input.Contains("show tasks"))
                return taskManager.DisplayTasks();

            if (input.Contains("complete task"))
            {
                int id = ExtractNumber(input);
                taskManager.CompleteTask(id);

                LogActivity("TASK COMPLETED: ID " + id);
                return "Task marked as completed.";
            }

            if (input.Contains("delete task"))
            {
                int id = ExtractNumber(input);
                taskManager.DeleteTask(id);

                LogActivity("TASK DELETED: ID " + id);
                return "Task deleted.";
            }

            // ================= QUIZ SYSTEM =================

            if (input.Contains("start quiz"))
            {
                LogActivity("QUIZ STARTED");

                var q = quizManager.GetCurrentQuestion();

                if (q == null)
                {
                    return $"Quiz finished! Score: {quizManager.GetScore()} / {quizManager.GetTotalQuestions()}";
                }

                return q.Question +
                       "\nA: " + q.OptionA +
                       "\nB: " + q.OptionB +
                       "\nC: " + q.OptionC +
                       "\nD: " + q.OptionD;
            }

            if (input.Contains("answer"))
            {
                string answer = input.Replace("answer", "").Trim().ToUpper();

                bool correct = quizManager.CheckAnswer(answer);

                LogActivity("QUIZ ANSWER: " + answer);

                return correct ? "Correct answer!" : "Wrong answer!";
            }

            // ================= SENTIMENT =================

            if (input.Contains("worried"))
                return "Stay safe: " + GetRandomResponse("password");

            if (input.Contains("frustrated"))
                return "Take it step by step. You're learning well.";

            if (input.Contains("curious"))
                return "Curiosity helps you grow in cybersecurity.";

            // ================= HELP =================

            if (input.Contains("help"))
            {
                return
                "COMMANDS:\n" +
                "- add task / remind me\n" +
                "- show tasks\n" +
                "- complete task <id>\n" +
                "- delete task <id>\n" +
                "- start quiz\n" +
                "- answer A/B/C/D\n" +
                "- activity log";
            }

            // ================= KEYWORDS =================

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    currentTopic = keyword;
                    return GetRandomResponse(keyword);
                }
            }

            return "I didn't understand that. Type 'help' for commands.";
        }

        // ================= HELPERS =================

        private void LogActivity(string action)
        {
            activityLog.Add(DateTime.Now.ToString("HH:mm:ss") + " - " + action);

            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private string GetActivityLog()
        {
            if (activityLog.Count == 0)
                return "No activity yet.";

            string result = "RECENT ACTIVITY:\n";

            foreach (var log in activityLog)
                result += log + "\n";

            return result;
        }

        private string ExtractTaskText(string input)
        {
            input = input.ToLower();
            input = input.Replace("add task", "")
                         .Replace("remind me", "")
                         .Replace("set reminder", "");

            return input.Trim();
        }

        private int ExtractNumber(string input)
        {
            string number = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    number += c;
            }

            return number == "" ? -1 : int.Parse(number);
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> responses = keywordResponses[keyword];
            return responses[random.Next(responses.Count)];
        }
    }
}