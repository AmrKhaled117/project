using System;
using System.Collections.Generic;

namespace ExaminationSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var course = new Course { ID = 1, Title = "C# Basics", Description = "Intro to C#", MaxDegree = 100 };
            var student1 = new Student { ID = 1, Name = "Ahmed", Email = "ahmed@example.com" };
            var student2 = new Student { ID = 2, Name = "Amr", Email = "amr@example.com" };
            var instructor = new Instructor { ID = 1, Name = "Dr. ALi", Specialization = "Programming" };

            instructor.Courses.Add(course);

            var exam = new Exam { ID = 1, Title = "C# Exam", Course = course };
            course.Exams.Add(exam);

            exam.AddQuestion(new MultipleChoiceQuestion
            {
                Text = "What is C#?",
                Options = new List<string> { "Language", "Framework", "Database" },
                CorrectAnswer = "Language",
                Mark = 40
            });

            exam.AddQuestion(new TrueFalseQuestion
            {
                Text = "C# is developed by Microsoft.",
                CorrectAnswer = true,
                Mark = 30
            });

            exam.AddQuestion(new EssayQuestion
            {
                Text = "Explain OOP in C#.",
                Mark = 30
            });

            var answers1 = new Dictionary<int, string>
            {
                { 0, "Language" },
                { 1, "true" },
                { 2, "OOP is about abstraction, inheritance, etc." }
            };

            var answers2 = new Dictionary<int, string>
            {
                { 0, "Database" },
                { 1, "false" },
                { 2, "OOP concepts" }
            };

            int score1 = exam.TakeExam(student1, answers1);
            int score2 = exam.TakeExam(student2, answers2);

            Report.ShowReport(exam, student1);
            Console.WriteLine("------------");
            Report.ShowReport(exam, student2);
            Console.WriteLine("------------");
            Report.CompareStudents(exam, student1, student2);
        }
    }
}