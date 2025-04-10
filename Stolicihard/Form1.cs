using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Stolicihard
{
    public partial class Form1 : Form
    {
        private List<(string Country, string CorrectCapital, string[] Options)> questions;
        private int currentQuestionIndex;
        private int correctAnswers;
        private string selectedDifficulty;
        private Button[] answerButtons;

        public Form1()
        {
            InitializeComponent();
            ShowDifficultySelection();
        }

        private void ShowDifficultySelection()
        {
            Controls.Clear();
            Label label = new Label
            {
                Text = "Выберите сложность",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(100, 50),
                AutoSize = true
            };

            Button easyButton = new Button { Text = "Легкий", Location = new Point(100, 100), Width = 200, Height = 60, Font = new Font("Arial", 14, FontStyle.Bold) };
            Button mediumButton = new Button { Text = "Средний", Location = new Point(100, 180), Width = 200, Height = 60, Font = new Font("Arial", 14, FontStyle.Bold) };
            Button hardButton = new Button { Text = "Сложный", Location = new Point(100, 260), Width = 200, Height = 60, Font = new Font("Arial", 14, FontStyle.Bold) };

            easyButton.Click += (s, e) => StartTest("Легкий");
            mediumButton.Click += (s, e) => StartTest("Средний");
            hardButton.Click += (s, e) => StartTest("Сложный");

            Controls.Add(label);
            Controls.Add(easyButton);
            Controls.Add(mediumButton);
            Controls.Add(hardButton);
        }

        private void StartTest(string difficulty)
        {
            selectedDifficulty = difficulty;
            currentQuestionIndex = 0;
            correctAnswers = 0;
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<(string, string, string[])>
            {
                // 30 вопросов для сложного уровня
                ("Франция", "Париж", new[] { "Берлин", "Мадрид", "Париж", "Рим" }),
                ("Германия", "Берлин", new[] { "Берлин", "Лондон", "Варшава", "Прага" }),
                ("Франция", "Париж", new[] { "Берлин", "Мадрид", "Париж", "Рим" }),
                ("Германия", "Берлин", new[] { "Берлин", "Лондон", "Варшава", "Прага" }),
                ("Италия", "Рим", new[] { "Афины", "Рим", "Лиссабон", "Осло" }),
                ("Испания", "Мадрид", new[] { "Мадрид", "Брюссель", "Копенгаген", "Дублин" }),
                ("Россия", "Москва", new[] { "Москва", "Минск", "Киев", "Баку" }),
                ("Япония", "Токио", new[] { "Сеул", "Токио", "Пекин", "Бангкок" }),
                ("США", "Вашингтон", new[] { "Лос-Анджелес", "Нью-Йорк", "Вашингтон", "Чикаго" }),
                ("Канада", "Оттава", new[] { "Оттава", "Торонто", "Монреаль", "Ванкувер" }),
                ("Бразилия", "Бразилиа", new[] { "Буэнос-Айрес", "Рио-де-Жанейро", "Бразилиа", "Сан-Паулу" }),
                ("Великобритания", "Лондон", new[] { "Лондон", "Манчестер", "Бирмингем", "Эдинбург" }),
                ("Китай", "Пекин", new[] { "Шанхай", "Гонконг", "Пекин", "Тайбэй" }),
                ("Австралия", "Канберра", new[] { "Сидней", "Мельбурн", "Перт", "Канберра" }),
                ("Мексика", "Мехико", new[] { "Гвадалахара", "Мехико", "Монтеррей", "Тихуана" }),
                ("Аргентина", "Буэнос-Айрес", new[] { "Кордова", "Розарио", "Буэнос-Айрес", "Мендоса" }),
                ("Индия", "Нью-Дели", new[] { "Мумбаи", "Нью-Дели", "Бангалор", "Ченнай" }),
                ("Египет", "Каир", new[] { "Александрия", "Каир", "Гиза", "Луксор" }),
                ("Турция", "Анкара", new[] { "Стамбул", "Анкара", "Измир", "Анталья" }),
                ("Южная Корея", "Сеул", new[] { "Пусан", "Сеул", "Инчхон", "Тэгу" }),
                ("ЮАР", "Претория", new[] { "Кейптаун", "Йоханнесбург", "Дурбан", "Претория" }),
                ("Швеция", "Стокгольм", new[] { "Гётеборг", "Мальмё", "Стокгольм", "Уппсала" }),
                ("Нидерланды", "Амстердам", new[] { "Роттердам", "Гаага", "Амстердам", "Эйндховен" }),
                ("Норвегия", "Осло", new[] { "Бергена", "Осло", "Тронхейм", "Ставангер" }),
                ("Финляндия", "Хельсинки", new[] { "Тампере", "Хельсинки", "Оулу", "Турку" }),
                ("Швейцария", "Берн", new[] { "Женева", "Цюрих", "Берн", "Лозанна" }),
                ("Португалия", "Лиссабон", new[] { "Порту", "Лиссабон", "Брага", "Коимбра" }),
                ("Дания", "Копенгаген", new[] { "Орхус", "Копенгаген", "Оденсе", "Ольборг" }),
                ("Чехия", "Прага", new[] { "Брно", "Прага", "Острава", "Пльзень" }),
                ("Польша", "Варшава", new[] { "Краков", "Варшава", "Вроцлав", "Лодзь" })
            };

            questions = questions.OrderBy(q => Guid.NewGuid()).ToList();
            int questionCount = selectedDifficulty == "Легкий" ? 10 : selectedDifficulty == "Средний" ? 20 : 30;
            questions = questions.Take(questionCount).ToList();
        }

        private void DisplayQuestion()
        {
            Controls.Clear();
            var question = questions[currentQuestionIndex];
            Label questionLabel = new Label { Text = $"Столица {question.Country}?", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(50, 30), AutoSize = true };
            Controls.Add(questionLabel);

            answerButtons = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                answerButtons[i] = new Button { Text = question.Options[i], Location = new Point(50, 70 + i * 60), Width = 250, Height = 50, Font = new Font("Arial", 14) };
                answerButtons[i].Click += CheckAnswer;
                Controls.Add(answerButtons[i]);
            }
        }

        private void CheckAnswer(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            var question = questions[currentQuestionIndex];
            bool isCorrect = clickedButton.Text == question.CorrectCapital;

            Label resultLabel = new Label { Text = isCorrect ? "Верный ответ!" : $"Неверно! Правильный ответ: {question.CorrectCapital}", ForeColor = isCorrect ? Color.Green : Color.Red, Location = new Point(50, 310), Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true };
            Controls.Add(resultLabel);

            if (isCorrect) correctAnswers++;
            foreach (var btn in answerButtons) btn.Enabled = false;

            Button nextButton = new Button { Text = "Далее", Location = new Point(50, 370), Width = 200, Height = 60, Font = new Font("Arial", 14, FontStyle.Bold) };
            nextButton.Click += NextQuestion;
            Controls.Add(nextButton);
        }

        private void NextQuestion(object sender, EventArgs e)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
                DisplayQuestion();
            else
                ShowResults();
        }

        private void ShowResults()
        {
            Controls.Clear();
            Label resultLabel = new Label { Text = $"Вы правильно ответили на {correctAnswers} из {questions.Count} вопросов!", Location = new Point(50, 50), Font = new Font("Arial", 18, FontStyle.Bold), AutoSize = true };
            Button restartButton = new Button { Text = "Вернуться к выбору сложности", Location = new Point(50, 160), Width = 300, Height = 60, Font = new Font("Arial", 14, FontStyle.Bold) };
            restartButton.Click += (s, e) => ShowDifficultySelection();

            Controls.Add(resultLabel);
            Controls.Add(restartButton);
        }
    }
}
