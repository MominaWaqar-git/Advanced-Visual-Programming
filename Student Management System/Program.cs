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

        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsFirstLogin { get; set; } = true;
    }

    class Teacher : User
    {
        public string Subject { get; set; }
        public List<Assignment> Assignments = new List<Assignment>();
        public List<Quiz> Quizzes = new List<Quiz>();
    }

    class Student : User
    {
        public List<Teacher> AssignedTeachers = new List<Teacher>();
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

        static string GetValidInput(string fieldName)
        {
            while (true)
            {
                Console.Write($"Enter {fieldName}: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine($"{fieldName} cannot be empty!");
            }
        }

        static string GetValidEmail()
        {
            while (true)
            {
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                    return email;

                Console.WriteLine("Invalid Email! Must contain '@'");
            }
        }

        static string GetValidCNIC()
        {
            while (true)
            {
                Console.Write("Enter CNIC (13 digits): ");
                string cnic = Console.ReadLine();

                if (cnic.Length == 13 && cnic.All(char.IsDigit))
                    return cnic;

                Console.WriteLine("Invalid CNIC! Must be 13 digits only.");
            }
        }

        static string GetValidPhone()
        {
            while (true)
            {
                Console.Write("Enter Phone (11 digits): ");
                string phone = Console.ReadLine();

                if (phone.Length == 11 && phone.All(char.IsDigit))
                    return phone;

                Console.WriteLine("Invalid Phone! Must be 11 digits.");
            }
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
                Console.WriteLine("3. Update Teacher");
                Console.WriteLine("4. Delete Teacher");
                Console.WriteLine("5. Add Student");
                Console.WriteLine("6. View/Search Students");
                Console.WriteLine("7. Update Student");
                Console.WriteLine("8. Delete Student");
                Console.WriteLine("9. Assign Teacher to Student");
                Console.WriteLine("10. Logout\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddTeacher(); break;
                    case "2": ViewSearchTeachers(); break;
                    case "3": UpdateTeacher(); break;
                    case "4": DeleteTeacher(); break;
                    case "5": AddStudent(); break;
                    case "6": ViewSearchStudents(); break;
                    case "7": UpdateStudent(); break;
                    case "8": DeleteStudent(); break;
                    case "9": AssignTeacherToStudent(); break;
                    case "10": ShowWelcomeScreen(); break;
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

            t.Name = GetValidInput("Name");

            Console.Write("Enter Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            t.Age = age;

            t.CNIC = GetValidCNIC();
            t.Address = GetValidInput("Address");
            t.Phone = GetValidPhone();
            t.Email = GetValidEmail();
            t.Subject = GetValidInput("Subject");

            t.Username = GetValidInput("Username");

            t.Password = "";
            t.IsFirstLogin = true;

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

            Console.Write("Search by Name or press ENTER to view all: ");
            string search = Console.ReadLine().ToLower();

            var list = string.IsNullOrEmpty(search) ? teachers :
                teachers.Where(t => t.Name.ToLower().Contains(search)).ToList();

            Console.WriteLine("\nID\t\tName\t\tAge\t\tSubject\t\tPhone\t\tEmail\t\tUsername");

            foreach (var t in list)
            {
                Console.WriteLine($"{t.ID}\t{t.Name}\t{t.Age}\t{t.Subject}\t{t.Phone}\t{t.Email}\t{t.Username}");
            }

            Console.ReadKey();
        }

        static void UpdateTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== UPDATE TEACHER ========\n");

            foreach (var t in teachers)
                Console.WriteLine($"ID: {t.ID} | Name: {t.Name}");

            Console.Write("\nEnter Teacher ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            var teacher = teachers.FirstOrDefault(t => t.ID == id);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();

                // 🔥 FULL INFO SHOW
                Console.WriteLine("---- CURRENT TEACHER INFO ----\n");
                Console.WriteLine($"1. Name: {teacher.Name}");
                Console.WriteLine($"2. Age: {teacher.Age}");
                Console.WriteLine($"3. CNIC: {teacher.CNIC}");
                Console.WriteLine($"4. Address: {teacher.Address}");
                Console.WriteLine($"5. Phone: {teacher.Phone}");
                Console.WriteLine($"6. Email: {teacher.Email}");
                Console.WriteLine($"7. Subject: {teacher.Subject}");
                Console.WriteLine("8. Exit Update\n");

                Console.Write("Select field to update: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        teacher.Name = GetValidInput("Name");
                        break;

                    case "2":
                        Console.Write("Enter Age: ");
                        int.TryParse(Console.ReadLine(), out int age);
                        teacher.Age = age;
                        break;

                    case "3":
                        teacher.CNIC = GetValidCNIC();
                        break;

                    case "4":
                        teacher.Address = GetValidInput("Address");
                        break;

                    case "5":
                        teacher.Phone = GetValidPhone();
                        break;

                    case "6":
                        teacher.Email = GetValidEmail();
                        break;

                    case "7":
                        teacher.Subject = GetValidInput("Subject");
                        break;

                    case "8":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("\nUpdated successfully!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }


        static void DeleteTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE TEACHER ========\n");

            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.");
                Console.ReadKey();
                return;
            }

            foreach (var t in teachers)
            {
                Console.WriteLine($"ID: {t.ID}");
                Console.WriteLine($"Name: {t.Name}");
                Console.WriteLine($"Age: {t.Age}");
                Console.WriteLine($"CNIC: {t.CNIC}");
                Console.WriteLine($"Phone: {t.Phone}");
                Console.WriteLine($"Email: {t.Email}");
                Console.WriteLine($"Subject: {t.Subject}");
                Console.WriteLine("---------------------------");
            }

            Console.Write("\nEnter Teacher ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            var teacher = teachers.FirstOrDefault(t => t.ID == id);

            if (teacher != null)
            {
                Console.Write("Are you sure? (yes/no): ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "yes")
                {
                    teachers.Remove(teacher);
                    Console.WriteLine("Teacher deleted successfully!");
                }
            }
            else
            {
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

            s.Name = GetValidInput("Name");

            Console.Write("Enter Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            s.Age = age;

            s.CNIC = GetValidCNIC();
            s.Address = GetValidInput("Address");
            s.Phone = GetValidPhone();
            s.Email = GetValidEmail();

            s.Username = GetValidInput("Username");

            s.Password = "";
            s.IsFirstLogin = true;

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

            Console.Write("Search by Name or press ENTER to view all: ");
            string search = Console.ReadLine().ToLower();

            var list = string.IsNullOrEmpty(search) ? students :
                students.Where(s => s.Name.ToLower().Contains(search)).ToList();

            Console.WriteLine("\nID\tName\tAge\tPhone\tEmail\tAssigned Teacher");

            foreach (var s in list)
            {
                string teacherName = s.AssignedTeachers.Count > 0
                    ? string.Join(", ", s.AssignedTeachers.Select(t => t.Name))
                    : "Not Assigned";

                Console.WriteLine($"{s.ID}\t{s.Name}\t{s.Age}\t{s.Phone}\t{s.Email}\t{teacherName}");
            }

            Console.ReadKey();
        }

        static void UpdateStudent()
        {
            Console.Clear();
            Console.WriteLine("======== UPDATE STUDENT ========\n");

            foreach (var s in students)
                Console.WriteLine($"ID: {s.ID} | Name: {s.Name}");

            Console.Write("\nEnter Student ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            var student = students.FirstOrDefault(s => s.ID == id);

            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();

                // 🔥 FULL INFO SHOW
                Console.WriteLine("---- CURRENT STUDENT INFO ----\n");
                Console.WriteLine($"1. Name: {student.Name}");
                Console.WriteLine($"2. Age: {student.Age}");
                Console.WriteLine($"3. CNIC: {student.CNIC}");
                Console.WriteLine($"4. Address: {student.Address}");
                Console.WriteLine($"5. Phone: {student.Phone}");
                Console.WriteLine($"6. Email: {student.Email}");
                Console.WriteLine("7. Exit Update\n");

                Console.Write("Select field to update: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        student.Name = GetValidInput("Name");
                        break;

                    case "2":
                        Console.Write("Enter Age: ");
                        int.TryParse(Console.ReadLine(), out int age);
                        student.Age = age;
                        break;

                    case "3":
                        student.CNIC = GetValidCNIC();
                        break;

                    case "4":
                        student.Address = GetValidInput("Address");
                        break;

                    case "5":
                        student.Phone = GetValidPhone();
                        break;

                    case "6":
                        student.Email = GetValidEmail();
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("\nUpdated successfully!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }
        static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE STUDENT ========\n");

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.ID}");
                Console.WriteLine($"Name: {s.Name}");
                Console.WriteLine($"Age: {s.Age}");
                Console.WriteLine($"CNIC: {s.CNIC}");
                Console.WriteLine($"Phone: {s.Phone}");
                Console.WriteLine($"Email: {s.Email}");
                Console.WriteLine($"Address: {s.Address}");
                Console.WriteLine("---------------------------");
            }

            Console.Write("\nEnter Student ID to delete: ");
            int.TryParse(Console.ReadLine(), out int id);

            var student = students.FirstOrDefault(s => s.ID == id);

            if (student != null)
            {
                Console.Write("Are you sure? (yes/no): ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "yes")
                {
                    students.Remove(student);
                    Console.WriteLine("Student deleted successfully!");
                }
            }
            else
            {
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

            Console.WriteLine("---- STUDENTS LIST ----");
            foreach (var s in students)
                Console.WriteLine($"ID: {s.ID} | Name: {s.Name} | Phone: {s.Phone}");

            Console.Write("\nEnter Student ID: ");
            int.TryParse(Console.ReadLine(), out int studentId);

            var student = students.FirstOrDefault(s => s.ID == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n---- TEACHERS LIST ----");
            foreach (var t in teachers)
                Console.WriteLine($"ID: {t.ID} | Name: {t.Name} | Subject: {t.Subject}");

            Console.Write("\nEnter Teacher ID: ");
            int.TryParse(Console.ReadLine(), out int teacherId);

            var teacher = teachers.FirstOrDefault(t => t.ID == teacherId);
            if (teacher == null)
            {
                Console.WriteLine("Teacher not found!");
                Console.ReadKey();
                return;
            }

            // duplicate assign na ho
            if (student.AssignedTeachers.Contains(teacher))
            {
                Console.WriteLine("Teacher already assigned!");
            }
            else
            {
                student.AssignedTeachers.Add(teacher);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Teacher {teacher.Name} assigned to {student.Name}!");
            }

            Console.ReadKey();
        }

        // ================= TEACHER LOGIN & PANEL =================
        static void TeacherLogin()
        {
            Console.Clear();
            Console.WriteLine("======== TEACHER LOGIN ========\n");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            var teacher = teachers.FirstOrDefault(t => t.Username == username);

            if (teacher == null)
            {
                Console.WriteLine("User not found!");
                Console.ReadKey();
                return;
            }

            if (teacher.IsFirstLogin)
            {
                Console.WriteLine("First time login - Set your password:");
                Console.Write("Enter new password: ");
                teacher.Password = Console.ReadLine();
                teacher.IsFirstLogin = false;

                Console.WriteLine("Password set successfully!");
                Console.ReadKey();
                TeacherPanel(teacher);
            }
            else
            {
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                if (teacher.Password == password)
                {
                    Console.WriteLine("Login Successful!");
                    Console.ReadKey();
                    TeacherPanel(teacher);
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                    Console.ReadKey();
                }
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
            Console.WriteLine("======= CREATE ASSIGNMENT =======\n");

            Assignment assignment = new Assignment();

            Console.Write("Enter Assignment Title: ");
            assignment.Title = Console.ReadLine();

            Console.Write("Enter Total Marks: ");
            int.TryParse(Console.ReadLine(), out int marks);
            assignment.TotalMarks = marks;

            Console.WriteLine("\nNow add questions for this assignment.");

            while (true)
            {
                Console.Write("Enter Question Description: ");
                string question = Console.ReadLine();

                if (string.IsNullOrEmpty(assignment.Description))
                    assignment.Description = "- " + question;
                else
                    assignment.Description += "\n- " + question;

                Console.Write("Add another question? (yes/no): ");
                string more = Console.ReadLine().ToLower();

                if (more != "yes")
                    break;
            }

            teacher.Assignments.Add(assignment);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAssignment created successfully!");
            Console.ReadKey();
        }

        static void UploadQuiz(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= UPLOAD QUIZ =======\n");

            while (true)
            {
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

                    while (true)
                    {
                        Console.Write("Correct Answer (A/B/C/D): ");
                        string ans = Console.ReadLine().ToUpper();

                        if (ans == "A" || ans == "B" || ans == "C" || ans == "D")
                        {
                            q.CorrectAnswer = ans;
                            break;
                        }

                        Console.WriteLine("Invalid option! Enter A, B, C or D.");
                    }
                }
                else
                {
                    q.IsMCQ = false;

                    Console.Write("Enter Expected Answer: ");
                    q.CorrectAnswer = Console.ReadLine();
                }

                Console.Write("Enter Total Marks: ");
                int.TryParse(Console.ReadLine(), out int marks);
                q.TotalMarks = marks;

                teacher.Quizzes.Add(q);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nQuestion added successfully!");

                Console.ResetColor();

                Console.Write("\nDo you want to add another question? (yes/no): ");
                string more = Console.ReadLine().ToLower();

                if (more != "yes")
                    break;
            }
        }

        static void ViewStudentSubmissions(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= STUDENT SUBMISSIONS =======\n");

            var myStudents = students.Where(s => s.AssignedTeachers.Contains(teacher)).ToList();
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
                    if (teacher.Assignments.Any(a => a.ID == sub.Assignment.ID))
                        Console.WriteLine($"Assignment: {sub.Assignment.Title} - Submitted: {sub.SubmissionText} - Marks: {(sub.MarksObtained==-1 ? "Not Graded" : sub.MarksObtained.ToString())}");
                foreach (var qsub in s.QuizSubmissions)
                    if (teacher.Quizzes.Any(q => q.ID == qsub.Quiz.ID))
                        Console.WriteLine($"Quiz: {qsub.Quiz.Question} - Answer: {qsub.Answer} - Marks: {(qsub.MarksObtained==-1 ? "Not Graded" : qsub.MarksObtained.ToString())}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void GradeSubmissions(Teacher teacher)
        {
            Console.Clear();
            Console.WriteLine("======= GRADE SUBMISSIONS =======\n");

            var myStudents = students.Where(s => s.AssignedTeachers.Contains(teacher)).ToList();
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

            var student = students.FirstOrDefault(s => s.Username == username);

            if (student == null)
            {
                Console.WriteLine("User not found!");
                Console.ReadKey();
                return;
            }

            if (student.IsFirstLogin)
            {
                Console.WriteLine("First time login - Set your password:");
                Console.Write("Enter new password: ");
                student.Password = Console.ReadLine();
                student.IsFirstLogin = false;

                Console.WriteLine("Password set successfully!");
                Console.ReadKey();
                StudentPanel(student);
            }
            else
            {
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                if (student.Password == password)
                {
                    Console.WriteLine("Login Successful!");
                    Console.ReadKey();
                    StudentPanel(student);
                }
                else
                {
                    Console.WriteLine("Wrong Password!");
                    Console.ReadKey();
                }
            }
        }

        static void StudentPanel(Student student)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {student.Name} (Student)\n");

                Console.WriteLine("1. Submit Assignment");
                Console.WriteLine("2. Submit Quiz");
                Console.WriteLine("3. View Marks");
                Console.WriteLine("4. Logout\n");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": SubmitAssignment(student); break;
                    case "2": SolveQuiz(student); break;
                    case "3": ViewMarks(student); break;
                    case "4": ShowWelcomeScreen(); break;
                }
            }
        }

        static void SubmitAssignment(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= ASSIGNMENTS =======\n");

            if (student.AssignedTeachers.Count == 0)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            // Sirf wo assignments jo abhi submit nahi hue
            var assignments = student.AssignedTeachers
                .SelectMany(t => t.Assignments)
                .ToList();

            if (assignments.Count == 0)
            {
                Console.WriteLine("No new assignments available.");
                Console.ReadKey();
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.Clear();
                Console.WriteLine($"Assignment ID: {assignment.ID}");
                Console.WriteLine($"Title: {assignment.Title}");
                Console.WriteLine($"Description: {assignment.Description}");
                Console.WriteLine($"Total Marks: {assignment.TotalMarks}\n");

                Console.Write("Enter your submission text: ");
                string submissionText = Console.ReadLine();

                student.AssignmentSubmissions.Add(new AssignmentSubmission
                {
                    Assignment = assignment,
                    SubmissionText = submissionText
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAssignment submitted successfully!");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to go to next assignment...");
                Console.ReadKey();
            }

            Console.WriteLine("\nAll assignments submitted!");
            Console.ReadKey();
        }

        static void SolveQuiz(Student student)
        {
            Console.Clear();
            Console.WriteLine("======= SOLVE QUIZ =======\n");

            if (student.AssignedTeachers.Count == 0)
            {
                Console.WriteLine("No teacher assigned yet!");
                Console.ReadKey();
                return;
            }

            var quizzes = student.AssignedTeachers
                .SelectMany(t => t.Quizzes)
                .ToList();

            if (quizzes.Count == 0)
            {
                Console.WriteLine("No new quizzes available.");
                Console.ReadKey();
                return;
            }

            int totalMarks = 0;
            int obtainedMarks = 0;

            foreach (var quiz in quizzes)
            {
                Console.Clear();
                Console.WriteLine($"Question ID: {quiz.ID}");
                Console.WriteLine(quiz.Question);

                if (quiz.IsMCQ)
                {
                    Console.WriteLine($"A) {quiz.OptionA}");
                    Console.WriteLine($"B) {quiz.OptionB}");
                    Console.WriteLine($"C) {quiz.OptionC}");
                    Console.WriteLine($"D) {quiz.OptionD}");

                    Console.Write("Enter answer (A/B/C/D): ");
                    string answer = Console.ReadLine().ToUpper();

                    int marks = 0;
                    if (answer == quiz.CorrectAnswer)
                        marks = quiz.TotalMarks;

                    obtainedMarks += marks;
                    totalMarks += quiz.TotalMarks;

                    student.QuizSubmissions.Add(new QuizSubmission
                    {
                        Quiz = quiz,
                        Answer = answer,
                        MarksObtained = marks
                    });
                }
                else
                {
                    Console.Write("Enter descriptive answer: ");
                    string answer = Console.ReadLine();

                    totalMarks += quiz.TotalMarks;

                    student.QuizSubmissions.Add(new QuizSubmission
                    {
                        Quiz = quiz,
                        Answer = answer
                    });
                }

                Console.WriteLine("\nPress any key for next question...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("Quiz Completed!");
            Console.WriteLine($"Total Marks: {obtainedMarks}/{totalMarks}");
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