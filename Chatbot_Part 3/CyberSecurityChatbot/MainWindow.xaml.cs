using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatbot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot;
        private DatabaseManager db;

        public MainWindow()
        {
            InitializeComponent();

            chatbot = new Chatbot();
            db = new DatabaseManager();

            try
            {
                VoicePlayer.PlayGreeting();
            }
            catch
            {
                // ignore
            }

            AppendMessage("BOT", "Hello! Welcome to the Cybersecurity Awareness Bot.");
            AppendMessage("BOT", "What is your name?");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ProcessInput();
        }

        private void ProcessInput()
        {
            string input = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                AppendMessage("BOT", "Please enter something.");
                return;
            }

            AppendMessage("YOU", input);

            string lowerInput = input.ToLower();

            // =========================
            // NEW: SHOW TASKS COMMAND
            // =========================
            if (lowerInput.Contains("show task"))
            {
                ShowTasks();
                UserInput.Clear();
                return;
            }

            string response;

            try
            {
                response = chatbot.GetResponse(input);
            }
            catch (Exception ex)
            {
                response = "Error: " + ex.Message;
            }

            AppendMessage("BOT", response);

            UserInput.Clear();
        }

        // =========================
        // SHOW TASKS FROM MYSQL
        // =========================
        private void ShowTasks()
{
    DataTable tasks = db.GetTasks();

    if (tasks.Rows.Count == 0)
    {
        AppendMessage("BOT", "No tasks found.");
        return;
    }

    AppendMessage("BOT", "Here are your tasks:");

    for (int i = 0; i < tasks.Rows.Count; i++)
    {
        DataRow row = tasks.Rows[i];

        string task =
            "TaskName: " + row["TaskName"] + "\n" +
            "Description: " + row["Description"] + "\n" +
            "ReminderDate: " + row["ReminderDate"] + "\n" +
            "Completed: " + row["IsCompleted"];

        AppendMessage("BOT", task);
    }
}

        private void AppendMessage(string sender, string message)
        {
            Border bubble = new Border
            {
                CornerRadius = new CornerRadius(18),
                Padding = new Thickness(12),
                Margin = new Thickness(6),
                MaxWidth = 420
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap
            };

            StackPanel wrapper = new StackPanel();

            if (sender == "YOU")
            {
                bubble.Background = new SolidColorBrush(Color.FromRgb(37, 211, 102));
                text.Foreground = Brushes.Black;
                wrapper.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else
            {
                bubble.Background = new SolidColorBrush(Color.FromRgb(50, 50, 50));
                text.Foreground = Brushes.White;
                wrapper.HorizontalAlignment = HorizontalAlignment.Left;
            }

            bubble.Child = text;
            wrapper.Children.Add(bubble);

            ChatPanel.Children.Add(wrapper);

            ChatScrollViewer.Dispatcher.InvokeAsync(() =>
            {
                ChatScrollViewer.ScrollToEnd();
            });
        }
    }
}