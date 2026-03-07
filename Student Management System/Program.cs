using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem
{
    // ======================= CLASSES =======================
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    class Teacher : User
    {
        public string Subject { get; set; }
        public List<Assignment> Assignments = new List<Assignment>();
        public List<Quiz> Quizzes = new List<Quiz>();
    }

    class Student : User
    {
        public Teacher AssignedTeacher { get; set; }
        public List<AssignmentSubmission> AssignmentSubmissions = new List<AssignmentSubmission>();
        public List<QuizSubmission> QuizSubmissions = new List<QuizSubmission>();
    }

    class Assignment
    {
        private static int counter = 1;
        public int ID { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalMarks { get; set; }

        public Assignment()
        {
            ID = counter++;
        }
    }

    class Quiz
    {
        private static int counter = 1;
        public int ID { get; private set; }

        public string Question { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        public string CorrectAnswer { get; set; }

        public bool IsMCQ { get; set; }

        public int TotalMarks { get; set; }

        public Quiz()
        {
            ID = counter++;
        }
    }

    class AssignmentSubmission
    {
        public Assignment Assignment { get; set; }
        public string SubmissionText { get; set; }
        public int MarksObtained { get; set; } = -1;
    }

    class QuizSubmission
    {
        public Quiz Quiz { get; set; }
        public string Answer { get; set; }
        public int MarksObtained { get; set; } = -1;
    }

    // ======================= PROGRAM =======================
    class Program
    {
        static List<Teacher> teachers = new List<Teacher>();
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            ShowWelcomeScreen();
        }

        // ================= WELCOME SCREEN =================
        static void ShowWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==============================================");
            Console.WriteLine("      WELCOME TO STUDENT MANAGEMENT SYSTEM   ");
            Console.WriteLine("==============================================\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select your role:\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Teacher");
            Console.WriteLine("3. Student");
            Console.WriteLine("4. Exit\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter choice (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AdminLogin(); break;
                case "2": TeacherLogin(); break;
                case "3": StudentLogin(); break;
                case "4": Environment.Exit(0); break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice! Press any key to try again...");
                    Console.ReadKey();
                    ShowWelcomeScreen();
                    break;
            }
        }

        // ================= ADMIN LOGIN & PANEL =================
        static void AdminLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======== ADMIN LOGIN ========\n");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (username == "admin" && password == "admin123")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLogin Successful!");
                Console.ReadKey();
                AdminPanel();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Credentials! Press any key to try again...");
                Console.ReadKey();
                ShowWelcomeScreen();
            }
        }

        static void AdminPanel()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======== ADMIN PANEL ========\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Add Teacher");
                Console.WriteLine("2. View/Search Teachers");
                Console.WriteLine("3. Delete Teacher");
                Console.WriteLine("4. Add Student");
                Console.WriteLine("5. View/Search Students");
                Console.WriteLine("6. Delete Student");
                Console.WriteLine("7. Assign Teacher to Student");
                Console.WriteLine("8. Logout\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddTeacher(); break;
                    case "2": ViewSearchTeachers(); break;
                    case "3": DeleteTeacher(); break;
                    case "4": AddStudent(); break;
                    case "5": ViewSearchStudents(); break;
                    case "6": DeleteStudent(); break;
                    case "7": AssignTeacherToStudent(); break;
                    case "8": ShowWelcomeScreen(); break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice! Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ================= ADMIN FUNCTIONS =================
        static void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== ADD TEACHER ========\n");

            Teacher t = new Teacher();
            t.ID = teachers.Count + 1;

            Console.Write("Enter Name: ");
            t.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            t.Age = age;

            Console.Write("Enter Subject: ");
            t.Subject = Console.ReadLine();

            Console.Write("Set Username for login: ");
            t.Username = Console.ReadLine();
            Console.Write("Set Password: ");
            t.Password = Console.ReadLine();

            teachers.Add(t);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTeacher added successfully!");
            Console.ReadKey();
        }

        static void ViewSearchTeachers()
        {
            Console.Clear();
            Console.WriteLine("======== VIEW/SEARCH TEACHERS ========\n");

            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Search by Name or press ENTER to view all:");
            string search = Console.ReadLine().ToLower();

            var list = string.IsNullOrEmpty(search) ? teachers :
                teachers.Where(t => t.Name.ToLower().Contains(search)).ToList();

            Console.WriteLine("\nID\tName\tAge\tSubject\tUsername");
            foreach (var t in list)
                Console.WriteLine($"{t.ID}\t{t.Name}\t{t.Age}\t{t.Subject}\t{t.Username}");
            Console.ReadKey();
        }

        static void DeleteTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE TEACHER ========\n");

            ViewSearchTeachers();
            Console.Write("Enter Teacher ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            var teacher = teachers.FirstOrDefault(t => t.ID == id);
            if (teacher != null)
            {
                teachers.Remove(teacher);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Teacher deleted successfully!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Teacher not found!");
            }
            Console.ReadKey();
        }

        static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("======== ADD STUDENT ========\n");

            Student s = new Student();
            s.ID = students.Count + 1;

            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            s.Age = age;

            Console.Write("Set Username for login: ");
            s.Username = Console.ReadLine();
            Console.Write("Set Password: ");
            s.Password = Console.ReadLine();

            students.Add(s);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nStudent added successfully!");
            Console.ReadKey();
        }

        static void ViewSearchStudents()
        {
            Console.Clear();
            Console.WriteLine("======== VIEW/SEARCH STUDENTS ========\n");

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Search by Name or press ENTER to view all:");
            string search = Console.ReadLine().ToLower();

            var list = string.IsNullOrEmpty(search) ? students :
                students.Where(s => s.Name.ToLower().Contains(search)).ToList();

            Console.WriteLine("\nID\tName\tAge\tAssigned Teacher\tUsername");
            foreach (var s in list)
            {
                string teacherName = s.AssignedTeacher != null ? s.AssignedTeacher.Name : "Not Assigned";
                Console.WriteLine($"{s.ID}\t{s.Name}\t{s.Age}\t{teacherName}\t{s.Username}");
            }
            Console.ReadKey();
        }

        static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE STUDENT ========\n");

            ViewSearchStudents();
            Console.Write("Enter Student ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            var student = students.FirstOrDefault(s => s.ID == id);
            if (student != null)
            {
                students.Remove(student);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Student deleted successfully!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student not found!");
            }
            Console.ReadKey();
        }

        static void AssignTeacherToStudent()
        {
            Console.Clear();
            Console.WriteLine("======== ASSIGN TEACHER TO STUDENT ========\n");

            if (teachers.Count == 0 || students.Count == 0)
            {
                Console.WriteLine("Teachers or Students list is empty!");
                Console.ReadKey();
                return;
            }

            ViewSearchStudents();
            Console.Write("Enter Student ID to assign teacher: ");
            int.TryParse(Console.ReadLine(), out int studentId);

            var student = students.FirstOrDefault(s => s.ID == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            ViewSearchTeachers();
            Console.Write("Enter Teacher ID to assign: ");
            int.TryParse(Console.ReadLine(), out int teacherId);

            var teacher = teachers.FirstOrDefault(t => t.ID == teacherId);
            if (teacher == null)
            {
                Console.WriteLine("Teacher not found!");
                Console.ReadKey();
                return;
            }

            student.AssignedTeacher = teacher;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Teacher {teacher.Name} assigned to Student {student.Name} successfully!");
            Console.ReadKey();
        }

        // ================= TEACHER LOGIN & PANEL =================
        static void TeacherLogin()
        {
            Console.Clear();
            Console.WriteLine("======== TEACHER LOGIN ========\n");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var teacher = teachers.FirstOrDefault(t => t.Username == username && t.Password == password);
            if (teacher != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLogin Successful!");
                Console.ReadKey();
                TeacherPanel(teacher);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Credentials! Press any key...");
                Console.ReadKey();
                ShowWelcomeScreen();
            }
        }

        static void TeacherPanel(Teacher loggedInTeacher)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {loggedInTeacher.Name} (Teacher)\n");

                Console.WriteLine("1. Upload Assignment");
                Console.WriteLine("2. Upload Quiz");
                Console.WriteLine("3. View Student Submissions");
                Console.WriteLine("4. Grade Submissions");
                Console.WriteLine("5. Logout\n");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": UploadAssignment(loggedInTeacher); break;
                    case "2": UploadQuiz(loggedInTeacher); break;
                    case "3": ViewStudentSubmissions(loggedInTeacher); break;
                    case "4": GradeSubmissions(loggedInTeacher); break;
                    case "5": ShowWelcomeScreen(); break;
                }
            }
        }

        static void UploadAssignment(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= UPLOAD ASSIGNMENT =======\n");

            Assignment a = new Assignment();

            Console.Write("Enter Title: ");
            a.Title = Console.ReadLine();
            Console.Write("Enter Description: ");
            a.Description = Console.ReadLine();
            Console.Write("Enter Total Marks: ");
            int.TryParse(Console.ReadLine(), out int marks);
            a.TotalMarks = marks;

            teacher.Assignments.Add(a);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAssignment uploaded successfully!");
            Console.ReadKey();
        }

        static void UploadQuiz(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= UPLOAD QUIZ =======\n");

            Quiz q = new Quiz();

            Console.Write("Enter Question: ");
            q.Question = Console.ReadLine();

            Console.Write("Is this MCQ Quiz? (yes/no): ");
            string type = Console.ReadLine().ToLower();

            if (type == "yes")
            {
                q.IsMCQ = true;

                Console.Write("Option A: ");
                q.OptionA = Console.ReadLine();

                Console.Write("Option B: ");
                q.OptionB = Console.ReadLine();

                Console.Write("Option C: ");
                q.OptionC = Console.ReadLine();

                Console.Write("Option D: ");
                q.OptionD = Console.ReadLine();

                Console.Write("Correct Answer (A/B/C/D): ");
                q.CorrectAnswer = Console.ReadLine().ToUpper();
            }
            else
            {
                q.IsMCQ = false;

                Console.Write("Enter Expected Answer (for reference): ");
                q.CorrectAnswer = Console.ReadLine();
            }

            Console.Write("Enter Total Marks: ");
            int.TryParse(Console.ReadLine(), out int marks);
            q.TotalMarks = marks;

            teacher.Quizzes.Add(q);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nQuiz uploaded successfully!");
            Console.ReadKey();
        }

        static void ViewStudentSubmissions(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= STUDENT SUBMISSIONS =======\n");

            var myStudents = students.Where(s => s.AssignedTeacher == teacher).ToList();
            if (myStudents.Count == 0)
            {
                Console.WriteLine("No assigned students found!");
                Console.ReadKey();
                return;
            }

            foreach (var s in myStudents)
            {
                Console.WriteLine($"Student: {s.Name}");
                foreach (var sub in s.AssignmentSubmissions)
                    if (teacher.Assignments.Contains(sub.Assignment))
                        Console.WriteLine($"Assignment: {sub.Assignment.Title} - Submitted: {sub.SubmissionText} - Marks: {(sub.MarksObtained==-1 ? "Not Graded" : sub.MarksObtained.ToString())}");
                foreach (var qsub in s.QuizSubmissions)
                    if (teacher.Quizzes.Contains(qsub.Quiz))
                        Console.WriteLine($"Quiz: {qsub.Quiz.Question} - Answer: {qsub.Answer} - Marks: {(qsub.MarksObtained==-1 ? "Not Graded" : qsub.MarksObtained.ToString())}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void GradeSubmissions(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= GRADE SUBMISSIONS =======\n");

            var myStudents = students.Where(s => s.AssignedTeacher == teacher).ToList();
            foreach (var s in myStudents)
            {
                foreach (var sub in s.AssignmentSubmissions)
                {
                    if (teacher.Assignments.Contains(sub.Assignment) && sub.MarksObtained == -1)
                    {
                        Console.WriteLine($"Student: {s.Name} Assignment: {sub.Assignment.Title} Submission: {sub.SubmissionText}");
                        Console.Write("Enter Marks: ");
                        int.TryParse(Console.ReadLine(), out int m);
                        sub.MarksObtained = m;
                    }
                }

                foreach (var qsub in s.QuizSubmissions)
                {
                    if (teacher.Quizzes.Contains(qsub.Quiz) && qsub.MarksObtained == -1)
                    {
                        Console.WriteLine($"Student: {s.Name} Quiz: {qsub.Quiz.Question} Answer: {qsub.Answer}");
                        Console.Write("Enter Marks: ");
                        int.TryParse(Console.ReadLine(), out int m);
                        qsub.MarksObtained = m;
                    }
                }
            }
            Console.WriteLine("\nAll submissions graded!");
            Console.ReadKey();
        }

        // ================= STUDENT LOGIN & PANEL =================
        static void StudentLogin()
        {
            Console.Clear();
            Console.WriteLine("======== STUDENT LOGIN ========\n");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var student = students.FirstOrDefault(s => s.Username == username && s.Password == password);
            if (student != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLogin Successful!");
                Console.ReadKey();
                StudentPanel(student);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Credentials! Press any key...");
                Console.ReadKey();
                ShowWelcomeScreen();
            }
        }

        static void StudentPanel(Student student)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {student.Name} (Student)\n");

                Console.WriteLine("1. View Assignments");
                Console.WriteLine("2. Submit Assignment");
                Console.WriteLine("3. View Quizzes");
                Console.WriteLine("4. Solve Quiz");
                Console.WriteLine("5. View Marks");
                Console.WriteLine("6. Logout\n");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAssignments(student); break;
                    case "2": SubmitAssignment(student); break;
                    case "3": ViewQuizzes(student); break;
                    case "4": SolveQuiz(student); break;
                    case "5": ViewMarks(student); break;
                    case "6": ShowWelcomeScreen(); break;
                }
            }
        }

        static void ViewAssignments(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= ASSIGNMENTS =======\n");

            if (student.AssignedTeacher == null)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            if (student.AssignedTeacher.Assignments.Count == 0)
            {
                Console.WriteLine("No assignments uploaded yet!");
                Console.ReadKey();
                return;
            }

            foreach (var a in student.AssignedTeacher.Assignments)
                Console.WriteLine($"ID: {a.ID} Title: {a.Title} - Description: {a.Description} - Marks: {a.TotalMarks}");

            Console.ReadKey();
        }

        static void SubmitAssignment(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= SUBMIT ASSIGNMENT =======\n");

            if (student.AssignedTeacher == null)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            var assignments = student.AssignedTeacher.Assignments
                .Where(a => !student.AssignmentSubmissions.Any(sub => sub.Assignment.ID == a.ID))
                .ToList();

            if (assignments.Count == 0)
            {
                Console.WriteLine("No new assignments available to submit.");
                Console.ReadKey();
                return;
            }

            foreach (var a in assignments)
                Console.WriteLine($"ID: {a.ID} Title: {a.Title}");

            Console.Write("Enter Assignment ID to submit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey();
                return;
            }

            var assignment = assignments.FirstOrDefault(a => a.ID == id);
            if (assignment == null)
            {
                Console.WriteLine("Invalid Assignment ID or already submitted!");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter your submission text: ");
            string submission = Console.ReadLine();

            student.AssignmentSubmissions.Add(new AssignmentSubmission
            {
                Assignment = assignment,
                SubmissionText = submission
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Assignment submitted successfully!");
            Console.ReadKey();
        }

        static void ViewQuizzes(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= QUIZZES =======\n");

            if (student.AssignedTeacher == null)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            if (student.AssignedTeacher.Quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes uploaded yet!");
                Console.ReadKey();
                return;
            }

            foreach (var q in student.AssignedTeacher.Quizzes)
                Console.WriteLine($"ID: {q.ID} Question: {q.Question} - Marks: {q.TotalMarks}");

            Console.ReadKey();
        }

        static void SolveQuiz(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= SOLVE QUIZ =======\n");

            if (student.AssignedTeacher == null)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            var quizzes = student.AssignedTeacher.Quizzes
                .Where(q => !student.QuizSubmissions.Any(sub => sub.Quiz.ID == q.ID))
                .ToList();

            if (quizzes.Count == 0)
            {
                Console.WriteLine("No new quizzes available.");
                Console.ReadKey();
                return;
            }

            foreach (var q in quizzes)
                Console.WriteLine($"ID: {q.ID} Question: {q.Question}");

            Console.Write("Enter Quiz ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            var quiz = quizzes.FirstOrDefault(q => q.ID == id);

            if (quiz == null)
            {
                Console.WriteLine("Invalid Quiz ID!");
                Console.ReadKey();
                return;
            }

            if (quiz.IsMCQ)
            {
                Console.WriteLine($"\nA) {quiz.OptionA}");
                Console.WriteLine($"B) {quiz.OptionB}");
                Console.WriteLine($"C) {quiz.OptionC}");
                Console.WriteLine($"D) {quiz.OptionD}");

                Console.Write("Enter your answer (A/B/C/D): ");
                string answer = Console.ReadLine().ToUpper();

                int marks = 0;

                if (answer == quiz.CorrectAnswer)
                    marks = quiz.TotalMarks;

                student.QuizSubmissions.Add(new QuizSubmission
                {
                    Quiz = quiz,
                    Answer = answer,
                    MarksObtained = marks
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Quiz submitted! Marks: {marks}/{quiz.TotalMarks}");
            }
            else
            {
                Console.Write("Enter your descriptive answer: ");
                string answer = Console.ReadLine();

                student.QuizSubmissions.Add(new QuizSubmission
                {
                    Quiz = quiz,
                    Answer = answer
                });

                Console.WriteLine("Answer submitted! Teacher will grade it.");
            }

            Console.ReadKey();
        }

        static void ViewMarks(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= MARKS =======\n");

            foreach (var sub in student.AssignmentSubmissions)
            {
                Console.WriteLine($"Assignment: {sub.Assignment.Title} - Marks: {(sub.MarksObtained == -1 ? "Not Graded" : sub.MarksObtained.ToString())}");
            }

            foreach (var qsub in student.QuizSubmissions)
            {
                Console.WriteLine($"Quiz: {qsub.Quiz.Question} - Marks: {(qsub.MarksObtained == -1 ? "Not Graded" : qsub.MarksObtained.ToString())}");
            }

            Console.ReadKey();
        }
    }
}