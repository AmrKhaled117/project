using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystemApp
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();
        public Dictionary<int, int> ExamScores { get; set; } = new Dictionary<int, int>();
    }

    public class Instructor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }

    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxDegree { get; set; }
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }

    public abstract class Question
    {
        public string Text { get; set; }
        public int Mark { get; set; }
        public abstract bool CheckAnswer(string answer);
    }

    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public override bool CheckAnswer(string answer) => answer == CorrectAnswer;
    }

    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }
        public override bool CheckAnswer(string answer) => bool.TryParse(answer, out bool result) && result == CorrectAnswer;
    }

    public class EssayQuestion : Question
    {
        public override bool CheckAnswer(string answer) => true;
    }

    public class Exam
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public Course Course { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public bool Started { get; set; } = false;

        public void AddQuestion(Question q)
        {
            if (Started) return;
            int totalMarks = Questions.Sum(x => x.Mark) + q.Mark;
            if (totalMarks <= Course.MaxDegree) Questions.Add(q);
        }

        public int TakeExam(Student student, Dictionary<int, string> answers)
        {
            Started = true;
            int score = 0;
            for (int i = 0; i < Questions.Count; i++)
            {
                if (answers.ContainsKey(i) && Questions[i].CheckAnswer(answers[i]))
                    score += Questions[i].Mark;
            }
            student.ExamScores[ID] = score;
            return score;
        }
    }

    public class Report
    {
        public static void ShowReport(Exam exam, Student student)
        {
            int score = student.ExamScores.ContainsKey(exam.ID) ? student.ExamScores[exam.ID] : 0;
            Console.WriteLine($"Exam: {exam.Title}");
            Console.WriteLine($"Student: {student.Name}");
            Console.WriteLine($"Course: {exam.Course.Title}");
            Console.WriteLine($"Score: {score}/{exam.Course.MaxDegree}");
            Console.WriteLine(score >= exam.Course.MaxDegree * 0.5 ? "Status: Pass" : "Status: Fail");
        }

        public static void CompareStudents(Exam exam, Student s1, Student s2)
        {
            int score1 = s1.ExamScores.ContainsKey(exam.ID) ? s1.ExamScores[exam.ID] : 0;
            int score2 = s2.ExamScores.ContainsKey(exam.ID) ? s2.ExamScores[exam.ID] : 0;
            Console.WriteLine($"{s1.Name}: {score1}, {s2.Name}: {score2}");
            Console.WriteLine(score1 > score2 ? $"{s1.Name} performed better" :
                              score2 > score1 ? $"{s2.Name} performed better" : "Both students performed equally");
        }
    }
}