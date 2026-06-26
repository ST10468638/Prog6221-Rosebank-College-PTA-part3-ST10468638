using System;

namespace CyberSecurityChatbot
{
    public class QuizQuestion
    {
        public string Question { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        public string CorrectAnswer { get; set; }

        public QuizQuestion()
        {
            Question = "";
            OptionA = "";
            OptionB = "";
            OptionC = "";
            OptionD = "";
            CorrectAnswer = "";
        }
    }
}