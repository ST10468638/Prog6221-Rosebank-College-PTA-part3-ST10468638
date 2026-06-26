using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex;
        private int score;


    public QuizManager()
        {
            questions = new List<QuizQuestion>();

            LoadQuestions();

            currentQuestionIndex = 0;
            score = 0;
        }

        private void LoadQuestions()
        {
            questions.Add(new QuizQuestion
            {
                Question = "What is phishing?",
                OptionA = "A cyber attack",
                OptionB = "A web browser",
                OptionC = "An operating system",
                OptionD = "A password manager",
                CorrectAnswer = "A"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What should a strong password contain?",
                OptionA = "Only letters",
                OptionB = "Only numbers",
                OptionC = "Letters, numbers and symbols",
                OptionD = "Your name",
                CorrectAnswer = "C"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is two-factor authentication?",
                OptionA = "Extra security verification",
                OptionB = "A virus",
                OptionC = "A browser",
                OptionD = "A scam",
                CorrectAnswer = "A"
            });

            questions.Add(new QuizQuestion
            {
                Question = "Should you click unknown email links?",
                OptionA = "Always",
                OptionB = "Never",
                OptionC = "Sometimes",
                OptionD = "Only at work",
                CorrectAnswer = "B"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What does antivirus software do?",
                OptionA = "Creates viruses",
                OptionB = "Deletes files",
                OptionC = "Protects against malware",
                OptionD = "Changes passwords",
                CorrectAnswer = "C"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is malware?",
                OptionA = "Safe software",
                OptionB = "Malicious software",
                OptionC = "A firewall",
                OptionD = "An email",
                CorrectAnswer = "B"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is a scam?",
                OptionA = "A legitimate offer",
                OptionB = "A type of browser",
                OptionC = "A dishonest scheme",
                OptionD = "An operating system",
                CorrectAnswer = "C"
            });

            questions.Add(new QuizQuestion
            {
                Question = "Should you share passwords?",
                OptionA = "Yes",
                OptionB = "No",
                OptionC = "Only online",
                OptionD = "Only with strangers",
                CorrectAnswer = "B"
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is online privacy?",
                OptionA = "Protecting personal information",
                OptionB = "Deleting the internet",
                OptionC = "Creating malware",
                OptionD = "Sharing passwords",
                CorrectAnswer = "A"
            });

            questions.Add(new QuizQuestion
            {
                Question = "Why should software be updated?",
                OptionA = "To reduce security",
                OptionB = "To fix vulnerabilities",
                OptionC = "To create malware",
                OptionD = "No reason",
                CorrectAnswer = "B"
            });
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex];
            }

            return null;
        }

        public bool CheckAnswer(string answer)
        {
            if (currentQuestionIndex >= questions.Count)
            {
                return false;
            }

            bool correct =
                answer.ToUpper() ==
                questions[currentQuestionIndex].CorrectAnswer.ToUpper();

            if (correct)
            {
                score++;
            }

            currentQuestionIndex++;

            return correct;
        }

        public bool QuizFinished()
        {
            return currentQuestionIndex >= questions.Count;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetTotalQuestions()
        {
            return questions.Count;
        }
    }


}
