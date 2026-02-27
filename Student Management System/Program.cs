using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem
{
    // Base User class
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Teacher : User
    {
        public string Subject { get; set; }
    }

    class Student : User
    {
        public Teacher AssignedTeacher { get; set; }
    }

    class Program
    {
        // Lists to store Teachers and Students
        static List<Teacher> teachers = new List<Teacher>();
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            ShowWelcomeScreen();
        }

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

        // ======================= ADMIN FUNCTIONS =======================

        static void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== ADD TEACHER ========\n");

            Teacher t = new Teacher();
            t.ID = teachers.Count + 1;

            Console.Write("Enter Name: ");
            t.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            t.Age = int.Parse(Console.ReadLine());

            Console.Write("Enter Subject: ");
            t.Subject = Console.ReadLine();

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

            Console.WriteLine("\nID\tName\tAge\tSubject");
            foreach (var t in list)
            {
                Console.WriteLine($"{t.ID}\t{t.Name}\t{t.Age}\t{t.Subject}");
            }
            Console.ReadKey();
        }

        static void DeleteTeacher()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE TEACHER ========\n");

            ViewSearchTeachers();
            Console.Write("Enter Teacher ID to delete: ");
            int id = int.Parse(Console.ReadLine());

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
            s.Age = int.Parse(Console.ReadLine());

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

            Console.WriteLine("\nID\tName\tAge\tAssigned Teacher");
            foreach (var s in list)
            {
                string teacherName = s.AssignedTeacher != null ? s.AssignedTeacher.Name : "Not Assigned";
                Console.WriteLine($"{s.ID}\t{s.Name}\t{s.Age}\t{teacherName}");
            }
            Console.ReadKey();
        }

        static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("======== DELETE STUDENT ========\n");

            ViewSearchStudents();
            Console.Write("Enter Student ID to delete: ");
            int id = int.Parse(Console.ReadLine());

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
            int studentId = int.Parse(Console.ReadLine());

            var student = students.FirstOrDefault(s => s.ID == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            ViewSearchTeachers();
            Console.Write("Enter Teacher ID to assign: ");
            int teacherId = int.Parse(Console.ReadLine());

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

        static void TeacherLogin()
        {
            Console.Clear();
            Console.WriteLine("Teacher login coming soon...");
            Console.ReadKey();
            ShowWelcomeScreen();
        }

        static void StudentLogin()
        {
            Console.Clear();
            Console.WriteLine("Student login coming soon...");
            Console.ReadKey();
            ShowWelcomeScreen();
        }
    }
}