using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem
{
    // ======================= CLASSES =======================
    class DB
    {
        private static string connStr = "server=localhost;database=StudentDB;uid=root;pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }
    }
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
        static void Main(string[] args)
        {
            ShowWelcomeScreen();
        }

        // ================= VALIDATIONS SAME =================
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

                Console.WriteLine("Invalid Email!");
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

                Console.WriteLine("Invalid CNIC!");
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

                Console.WriteLine("Invalid Phone!");
            }
        }

        // ================= WELCOME =================
        static void ShowWelcomeScreen()
        {
            Console.Clear();

            // 🎨 Title Design
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==============================================");
            Console.WriteLine("   WELCOME TO STUDENT MANAGEMENT SYSTEM");
            Console.WriteLine("==============================================");
            Console.ResetColor();

            // 🎨 Menu
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1. Admin");
            Console.WriteLine("2. Teacher");
            Console.WriteLine("3. Student");
            Console.WriteLine("4. Exit");
            Console.ResetColor();

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AdminLogin();
                    break;

                case "2":
                    Console.Clear();
                    TeacherLogin();
                    break;

                case "3":
                    Console.Clear();
                    StudentLogin();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Invalid Choice! Try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    ShowWelcomeScreen();
                    break;
            }
        }

        static void AdminLogin()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================");
            Console.WriteLine("         ADMIN LOGIN");
            Console.WriteLine("=================================");
            Console.ResetColor();

            Console.Write("\nUsername: ");
            string u = Console.ReadLine();

            Console.Write("Password: ");
            string p = Console.ReadLine();

            if (u == "admin" && p == "admin123")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Login Successful!");
                Console.ResetColor();
                Console.ReadKey();
                AdminPanel();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Invalid Username or Password!");
                Console.ResetColor();
                Console.ReadKey();
                ShowWelcomeScreen();
            }
        }

        static void AdminPanel()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==============================================");
                Console.WriteLine("              ADMIN PANEL");
                Console.WriteLine("==============================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n1. Add Teacher");
                Console.WriteLine("2. View/Search Teacher");
                Console.WriteLine("3. Update Teacher");
                Console.WriteLine("4. Delete Teacher");
                Console.WriteLine("5. Add Student");
                Console.WriteLine("6. View/Search Student");
                Console.WriteLine("7. Update Student");
                Console.WriteLine("8. Delete Student");
                Console.WriteLine("9. Assign Teacher to Student");
                Console.WriteLine("10. Logout");
                Console.ResetColor();

                Console.Write("\nEnter your choice: ");
                string ch = Console.ReadLine();

                switch (ch)
                {
                    case "1":
                        Console.Clear();
                        AddTeacher();
                        break;

                    case "2":
                        Console.Clear();
                        ViewSearchTeachers();
                        break;

                    case "3":
                        Console.Clear();
                        UpdateTeacher();
                        break;

                    case "4":
                        Console.Clear();
                        DeleteTeacher();
                        break;

                    case "5":
                        Console.Clear();
                        AddStudent();
                        break;

                    case "6":
                        Console.Clear();
                        ViewSearchStudents();
                        break;

                    case "7":
                        Console.Clear();
                        UpdateStudent();
                        break;

                    case "8":
                        Console.Clear();
                        DeleteStudent();
                        break;

                    case "9":
                        Console.Clear();
                        AssignTeacherToStudent();
                        break;

                    case "10":
                        ShowWelcomeScreen();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Invalid Choice! Try again...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ================= TEACHER =================
        static void AddTeacher()
        {
            Console.Clear();

            // 🎨 Heading
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================");
            Console.WriteLine("         ADD TEACHER");
            Console.WriteLine("=================================");
            Console.ResetColor();

            Teacher t = new Teacher();

            t.Name = GetValidInput("Name");

            // ✅ AGE VALIDATION FIX
            while (true)
            {
                Console.Write("Enter Age: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int age) && age > 0)
                {
                    t.Age = age;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid Age! Enter a valid number.");
                Console.ResetColor();
            }

            t.CNIC = GetValidCNIC();
            t.Address = GetValidInput("Address");
            t.Phone = GetValidPhone();
            t.Email = GetValidEmail();
            t.Subject = GetValidInput("Subject");
            t.Username = GetValidInput("Username");

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = @"INSERT INTO Teachers 
        (Name,Age,CNIC,Address,Phone,Email,Username,Password,Subject)
        VALUES (@Name,@Age,@CNIC,@Address,@Phone,@Email,@Username,'',@Subject)";

                MySqlCommand cmd = new MySqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@Name", t.Name);
                cmd.Parameters.AddWithValue("@Age", t.Age);
                cmd.Parameters.AddWithValue("@CNIC", t.CNIC);
                cmd.Parameters.AddWithValue("@Address", t.Address);
                cmd.Parameters.AddWithValue("@Phone", t.Phone);
                cmd.Parameters.AddWithValue("@Email", t.Email);
                cmd.Parameters.AddWithValue("@Username", t.Username);
                cmd.Parameters.AddWithValue("@Subject", t.Subject);

                cmd.ExecuteNonQuery();
            }

            // ✅ Success Message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Teacher Added Successfully!");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ShowAllTeachers()
        {
            Console.Clear();

            // 🎨 Heading
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("           ALL TEACHERS");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Teachers";
                MySqlCommand cmd = new MySqlCommand(q, conn);

                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {r["ID"]}");
                    Console.ResetColor();

                    Console.WriteLine($"Name     : {r["Name"]}");
                    Console.WriteLine($"Age      : {r["Age"]}");
                    Console.WriteLine($"CNIC     : {r["CNIC"]}");
                    Console.WriteLine($"Address  : {r["Address"]}");
                    Console.WriteLine($"Phone    : {r["Phone"]}");
                    Console.WriteLine($"Email    : {r["Email"]}");
                    Console.WriteLine($"Username : {r["Username"]}");
                    Console.WriteLine($"Subject  : {r["Subject"]}");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("---------------------------------------");
                    Console.ResetColor();
                }
            }
        }


        static void ViewSearchTeachers()
        {
            ShowAllTeachers(); // 🔥 pehle sab show

            Console.Write("\nSearch by Name (press ENTER to skip): ");
            string s = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(s))
                return;

            Console.Clear();

            // 🎨 Heading
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("         SEARCH RESULT");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Teachers WHERE Name LIKE @s";
                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@s", "%" + s + "%");

                var r = cmd.ExecuteReader();

                bool found = false;

                while (r.Read())
                {
                    found = true;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {r["ID"]}");
                    Console.ResetColor();

                    Console.WriteLine($"Name     : {r["Name"]}");
                    Console.WriteLine($"Age      : {r["Age"]}");
                    Console.WriteLine($"CNIC     : {r["CNIC"]}");
                    Console.WriteLine($"Address  : {r["Address"]}");
                    Console.WriteLine($"Phone    : {r["Phone"]}");
                    Console.WriteLine($"Email    : {r["Email"]}");
                    Console.WriteLine($"Username : {r["Username"]}");
                    Console.WriteLine($"Subject  : {r["Subject"]}");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("---------------------------------------");
                    Console.ResetColor();
                }

                // ❌ No result found
                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No teacher found with this name!");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void UpdateTeacher()
        {
            ShowAllTeachers();

            // ✅ Safe ID input
            int id;
            while (true)
            {
                Console.Write("\nEnter Teacher ID to update: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid ID!");
                Console.ResetColor();
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ✅ Show selected teacher
                string selectQ = "SELECT * FROM Teachers WHERE ID=@id";
                MySqlCommand selectCmd = new MySqlCommand(selectQ, conn);
                selectCmd.Parameters.AddWithValue("@id", id);

                var r = selectCmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Teacher not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("======= TEACHER DETAILS =======\n");
                Console.ResetColor();

                Console.WriteLine($"ID       : {r["ID"]}");
                Console.WriteLine($"Name     : {r["Name"]}");
                Console.WriteLine($"Age      : {r["Age"]}");
                Console.WriteLine($"CNIC     : {r["CNIC"]}");
                Console.WriteLine($"Address  : {r["Address"]}");
                Console.WriteLine($"Phone    : {r["Phone"]}");
                Console.WriteLine($"Email    : {r["Email"]}");
                Console.WriteLine($"Username : {r["Username"]}");
                Console.WriteLine($"Subject  : {r["Subject"]}");

                r.Close();

                // 🔁 MULTI UPDATE LOOP
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nWhat do you want to update?");
                    Console.WriteLine("1.Name  2.Age  3.CNIC  4.Address  5.Phone  6.Email  7.Subject  8.Exit");
                    Console.ResetColor();

                    Console.Write("Enter choice: ");
                    string ch = Console.ReadLine();

                    string field = "";
                    object value = "";

                    switch (ch)
                    {
                        case "1":
                            field = "Name";
                            value = GetValidInput("Name");
                            break;

                        case "2":
                            field = "Age";
                            while (true)
                            {
                                Console.Write("Enter Age: ");
                                if (int.TryParse(Console.ReadLine(), out int age) && age > 0)
                                {
                                    value = age;
                                    break;
                                }
                                Console.WriteLine("❌ Invalid Age!");
                            }
                            break;

                        case "3":
                            field = "CNIC";
                            value = GetValidCNIC();
                            break;

                        case "4":
                            field = "Address";
                            value = GetValidInput("Address");
                            break;

                        case "5":
                            field = "Phone";
                            value = GetValidPhone();
                            break;

                        case "6":
                            field = "Email";
                            value = GetValidEmail();
                            break;

                        case "7":
                            field = "Subject";
                            value = GetValidInput("Subject");
                            break;

                        case "8":
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Invalid choice!");
                            Console.ResetColor();
                            continue;
                    }

                    // ✅ Update Query
                    string updateQ = $"UPDATE Teachers SET {field}=@val WHERE ID=@id";
                    MySqlCommand updateCmd = new MySqlCommand(updateQ, conn);

                    updateCmd.Parameters.AddWithValue("@val", value);
                    updateCmd.Parameters.AddWithValue("@id", id);

                    updateCmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Updated Successfully!");
                    Console.ResetColor();

                    // 🔁 Ask again
                    Console.Write("\nDo you want to update another field? (yes/no): ");
                    if (Console.ReadLine().ToLower() != "yes")
                        break;
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DeleteTeacher()
        {
            ShowAllTeachers();

            // ✅ Safe ID input
            int id;
            while (true)
            {
                Console.Write("\nEnter Teacher ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid ID!");
                Console.ResetColor();
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ✅ Show selected teacher
                string selectQ = "SELECT * FROM Teachers WHERE ID=@id";
                MySqlCommand selectCmd = new MySqlCommand(selectQ, conn);
                selectCmd.Parameters.AddWithValue("@id", id);

                var r = selectCmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Teacher not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======= DELETE TEACHER =======\n");
                Console.ResetColor();

                Console.WriteLine($"ID       : {r["ID"]}");
                Console.WriteLine($"Name     : {r["Name"]}");
                Console.WriteLine($"Subject  : {r["Subject"]}");
                Console.WriteLine($"Email    : {r["Email"]}");

                r.Close();

                // ⚠️ Confirmation
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nAre you sure you want to DELETE this teacher? (yes/no): ");
                Console.ResetColor();

                string confirm = Console.ReadLine().ToLower();

                if (confirm != "yes")
                {
                    Console.WriteLine("\n❌ Deletion Cancelled!");
                    Console.ReadKey();
                    return;
                }

                // ✅ Delete
                string deleteQ = "DELETE FROM Teachers WHERE ID=@id";
                MySqlCommand deleteCmd = new MySqlCommand(deleteQ, conn);
                deleteCmd.Parameters.AddWithValue("@id", id);

                deleteCmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Teacher Deleted Successfully!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ================= STUDENT =================
        static void AddStudent()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================");
            Console.WriteLine("         ADD STUDENT");
            Console.WriteLine("=================================");
            Console.ResetColor();

            Student s = new Student();

            s.Name = GetValidInput("Name");

            // AGE VALIDATION
            while (true)
            {
                Console.Write("Enter Age: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int age) && age > 0)
                {
                    s.Age = age;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid Age! Please enter a valid number.");
                Console.ResetColor();
            }

            s.CNIC = GetValidCNIC();
            s.Address = GetValidInput("Address");
            s.Phone = GetValidPhone();
            s.Email = GetValidEmail();

            // SUBJECT INPUT
            string subject = GetValidInput("Subject");

            s.Username = GetValidInput("Username");

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ✅ FIXED QUERY (SUBJECT ADDED)
                string q = @"INSERT INTO Students 
(Name,Age,CNIC,Address,Phone,Email,Username,Password,Subject)
VALUES (@Name,@Age,@CNIC,@Address,@Phone,@Email,@Username,'',@Subject)";

                MySqlCommand cmd = new MySqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@Name", s.Name);
                cmd.Parameters.AddWithValue("@Age", s.Age);
                cmd.Parameters.AddWithValue("@CNIC", s.CNIC);
                cmd.Parameters.AddWithValue("@Address", s.Address);
                cmd.Parameters.AddWithValue("@Phone", s.Phone);
                cmd.Parameters.AddWithValue("@Email", s.Email);
                cmd.Parameters.AddWithValue("@Username", s.Username);
                cmd.Parameters.AddWithValue("@Subject", subject); // 🔥 IMPORTANT FIX

                cmd.ExecuteNonQuery();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Student Added Successfully!");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ShowAllStudents()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("            ALL STUDENTS");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Students";
                MySqlCommand cmd = new MySqlCommand(q, conn);

                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {r["ID"]}");
                    Console.ResetColor();

                    Console.WriteLine($"Name     : {r["Name"]}");
                    Console.WriteLine($"Age      : {r["Age"]}");
                    Console.WriteLine($"CNIC     : {r["CNIC"]}");
                    Console.WriteLine($"Address  : {r["Address"]}");
                    Console.WriteLine($"Phone    : {r["Phone"]}");
                    Console.WriteLine($"Email    : {r["Email"]}");
                    Console.WriteLine($"Username : {r["Username"]}");

                    // ✅ SAFE SUBJECT DISPLAY (IMPORTANT FIX 🔥)
                    if (r["Subject"] != DBNull.Value && r["Subject"].ToString() != "")
                        Console.WriteLine($"Subject  : {r["Subject"]}");
                    else
                        Console.WriteLine("Subject  : Not Assigned");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("---------------------------------------");
                    Console.ResetColor();
                }
            }
        }

        static void ViewSearchStudents()
        {
            ShowAllStudents();

            Console.Write("\nSearch by Name (press ENTER to skip): ");
            string s = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(s))
                return;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("           SEARCH RESULT");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Students WHERE Name LIKE @s";
                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@s", "%" + s + "%");

                var r = cmd.ExecuteReader();

                bool found = false;

                while (r.Read())
                {
                    found = true;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {r["ID"]}");
                    Console.ResetColor();

                    Console.WriteLine($"Name     : {r["Name"]}");
                    Console.WriteLine($"Age      : {r["Age"]}");
                    Console.WriteLine($"CNIC     : {r["CNIC"]}");
                    Console.WriteLine($"Address  : {r["Address"]}");
                    Console.WriteLine($"Phone    : {r["Phone"]}");
                    Console.WriteLine($"Email    : {r["Email"]}");
                    Console.WriteLine($"Username : {r["Username"]}");

                    // ✅ SUBJECT FIX
                    if (r["Subject"] != DBNull.Value && r["Subject"].ToString() != "")
                        Console.WriteLine($"Subject  : {r["Subject"]}");
                    else
                        Console.WriteLine("Subject  : Not Assigned");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("---------------------------------------");
                    Console.ResetColor();
                }

                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ No student found with this name!");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void UpdateStudent()
        {
            ShowAllStudents();

            int id;
            while (true)
            {
                Console.Write("\nEnter Student ID to update: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid ID!");
                Console.ResetColor();
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string selectQ = "SELECT * FROM Students WHERE ID=@id";
                MySqlCommand selectCmd = new MySqlCommand(selectQ, conn);
                selectCmd.Parameters.AddWithValue("@id", id);

                var r = selectCmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Student not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("======= STUDENT DETAILS =======\n");
                Console.ResetColor();

                Console.WriteLine($"ID       : {r["ID"]}");
                Console.WriteLine($"Name     : {r["Name"]}");
                Console.WriteLine($"Age      : {r["Age"]}");
                Console.WriteLine($"CNIC     : {r["CNIC"]}");
                Console.WriteLine($"Address  : {r["Address"]}");
                Console.WriteLine($"Phone    : {r["Phone"]}");
                Console.WriteLine($"Email    : {r["Email"]}");
                Console.WriteLine($"Username : {r["Username"]}");

                if (r["Subject"] != DBNull.Value && r["Subject"].ToString() != "")
                    Console.WriteLine($"Subject  : {r["Subject"]}");
                else
                    Console.WriteLine("Subject  : Not Assigned");

                r.Close();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nEnter your choice to update:");
                    Console.WriteLine("1.Name  2.Age  3.CNIC  4.Address  5.Phone  6.Email  7.Subject  8.Exit");
                    Console.ResetColor();

                    Console.Write("Choice: ");
                    string ch = Console.ReadLine();

                    string field = "";
                    object value = "";

                    switch (ch)
                    {
                        case "1":
                            field = "Name";
                            value = GetValidInput("Name");
                            break;

                        case "2":
                            field = "Age";

                            while (true)
                            {
                                Console.Write("Enter Age: ");
                                if (int.TryParse(Console.ReadLine(), out int age) && age > 0)
                                {
                                    value = age;
                                    break;
                                }
                                Console.WriteLine("❌ Invalid Age!");
                            }
                            break;

                        case "3":
                            field = "CNIC";
                            value = GetValidCNIC();
                            break;

                        case "4":
                            field = "Address";
                            value = GetValidInput("Address");
                            break;

                        case "5":
                            field = "Phone";
                            value = GetValidPhone();
                            break;

                        case "6":
                            field = "Email";
                            value = GetValidEmail();
                            break;

                        case "7":
                            field = "Subject";
                            value = GetValidInput("Subject");
                            break;

                        case "8":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n🔙 Returning to Admin Panel...");
                            Console.ResetColor();
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Invalid choice!");
                            Console.ResetColor();
                            continue;
                    }

                    string updateQ = $"UPDATE Students SET {field}=@val WHERE ID=@id";
                    MySqlCommand updateCmd = new MySqlCommand(updateQ, conn);

                    updateCmd.Parameters.AddWithValue("@val", value);
                    updateCmd.Parameters.AddWithValue("@id", id);

                    updateCmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Updated Successfully!");
                    Console.ResetColor();
                }
            }
        }

        static void DeleteStudent()
        {
            ShowAllStudents();

            // ✅ Safe ID input
            int id;
            while (true)
            {
                Console.Write("\nEnter Student ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid ID!");
                Console.ResetColor();
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ✅ Show selected student
                string selectQ = "SELECT * FROM Students WHERE ID=@id";
                MySqlCommand selectCmd = new MySqlCommand(selectQ, conn);
                selectCmd.Parameters.AddWithValue("@id", id);

                var r = selectCmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Student not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======= DELETE STUDENT =======\n");
                Console.ResetColor();

                Console.WriteLine($"ID       : {r["ID"]}");
                Console.WriteLine($"Name     : {r["Name"]}");
                Console.WriteLine($"Age      : {r["Age"]}");
                Console.WriteLine($"Phone    : {r["Phone"]}");
                Console.WriteLine($"Email    : {r["Email"]}");
                Console.WriteLine($"Subject  : {r["Subject"]}");

                r.Close();

                // ⚠️ CONFIRMATION
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nAre you sure you want to DELETE this student? (yes/no): ");
                Console.ResetColor();

                string confirm = Console.ReadLine().ToLower();

                if (confirm != "yes")
                {
                    Console.WriteLine("\n❌ Deletion Cancelled!");
                    Console.ReadKey();
                    return;
                }

                // ✅ DELETE
                string deleteQ = "DELETE FROM Students WHERE ID=@id";
                MySqlCommand deleteCmd = new MySqlCommand(deleteQ, conn);
                deleteCmd.Parameters.AddWithValue("@id", id);

                deleteCmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Student Deleted Successfully!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        // ================= ASSIGN =================
        static void AssignTeacherToStudent()
        {
            Console.Clear();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ================= STUDENTS =================
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("======= STUDENTS (WITH SUBJECT & TEACHER) =======\n");
                Console.ResetColor();

                string qs = @"
        SELECT s.ID, s.Name, s.Subject,
               COALESCE(t.Name, 'Not Assigned') AS TeacherName
        FROM Students s
        LEFT JOIN StudentTeacher st ON s.ID = st.StudentID
        LEFT JOIN Teachers t ON t.ID = st.TeacherID";

                MySqlCommand cmdS = new MySqlCommand(qs, conn);
                var rs = cmdS.ExecuteReader();

                while (rs.Read())
                {
                    Console.WriteLine($"ID: {rs["ID"]} | Name: {rs["Name"]} | Subject: {rs["Subject"]} | Teacher: {rs["TeacherName"]}");
                }

                rs.Close();

                // ================= STUDENT SELECT =================
                Console.Write("\nEnter Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int sid))
                {
                    Console.WriteLine("❌ Invalid Student ID!");
                    Console.ReadKey();
                    return;
                }

                // GET STUDENT SUBJECT
                string stuSub = "";
                MySqlCommand getStu = new MySqlCommand("SELECT Subject FROM Students WHERE ID=@id", conn);
                getStu.Parameters.AddWithValue("@id", sid);
                var stuObj = getStu.ExecuteScalar();

                if (stuObj == null)
                {
                    Console.WriteLine("❌ Student not found!");
                    Console.ReadKey();
                    return;
                }

                stuSub = stuObj.ToString();

                Console.Clear();

                // ================= TEACHERS (FILTER BY SUBJECT) =================
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"======= TEACHERS (SUBJECT: {stuSub}) =======\n");
                Console.ResetColor();

                string qt = "SELECT ID, Name, Subject FROM Teachers WHERE Subject=@sub";
                MySqlCommand cmdT = new MySqlCommand(qt, conn);
                cmdT.Parameters.AddWithValue("@sub", stuSub);

                var rt = cmdT.ExecuteReader();

                bool hasTeacher = false;

                while (rt.Read())
                {
                    hasTeacher = true;
                    Console.WriteLine($"ID: {rt["ID"]} | Name: {rt["Name"]} | Subject: {rt["Subject"]}");
                }

                rt.Close();

                if (!hasTeacher)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ No teacher available for this subject!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                // ================= TEACHER SELECT =================
                Console.Write("\nEnter Teacher ID: ");
                if (!int.TryParse(Console.ReadLine(), out int tid))
                {
                    Console.WriteLine("❌ Invalid Teacher ID!");
                    Console.ReadKey();
                    return;
                }

                // VERIFY SUBJECT MATCH
                string checkTeachSub = "SELECT Subject FROM Teachers WHERE ID=@id";
                MySqlCommand chk = new MySqlCommand(checkTeachSub, conn);
                chk.Parameters.AddWithValue("@id", tid);

                var tsubObj = chk.ExecuteScalar();

                if (tsubObj == null)
                {
                    Console.WriteLine("❌ Teacher not found!");
                    Console.ReadKey();
                    return;
                }

                string teacherSub = tsubObj.ToString();

                if (teacherSub != stuSub)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Cannot assign! Subject mismatch.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                // ================= ASSIGN =================
                string check = "SELECT COUNT(*) FROM StudentTeacher WHERE StudentID=@s";
                MySqlCommand chk2 = new MySqlCommand(check, conn);
                chk2.Parameters.AddWithValue("@s", sid);

                int exists = Convert.ToInt32(chk2.ExecuteScalar());

                if (exists > 0)
                {
                    string update = "UPDATE StudentTeacher SET TeacherID=@t WHERE StudentID=@s";
                    MySqlCommand up = new MySqlCommand(update, conn);
                    up.Parameters.AddWithValue("@t", tid);
                    up.Parameters.AddWithValue("@s", sid);
                    up.ExecuteNonQuery();
                }
                else
                {
                    string insert = "INSERT INTO StudentTeacher (StudentID,TeacherID) VALUES (@s,@t)";
                    MySqlCommand ins = new MySqlCommand(insert, conn);
                    ins.Parameters.AddWithValue("@s", sid);
                    ins.Parameters.AddWithValue("@t", tid);
                    ins.ExecuteNonQuery();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Assigned Successfully (Same Subject Match)");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        // ================= TEACHER LOGIN & PANEL =================
        static void TeacherLogin()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================");
            Console.WriteLine("         TEACHER LOGIN");
            Console.WriteLine("===================================\n");
            Console.ResetColor();

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Teachers WHERE Username=@u";
                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@u", username);

                var r = cmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ User not found!");
                    Console.ResetColor();

                    Console.WriteLine("\nPress any key to go back...");
                    Console.ReadKey();
                    return;
                }

                int id = Convert.ToInt32(r["ID"]);
                string name = r["Name"].ToString();
                string password = r["Password"].ToString();

                r.Close();

                // ================= FIRST LOGIN =================
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n🔐 First Login - Set Your Password");
                    Console.ResetColor();

                    string newPass;

                    while (true)
                    {
                        Console.Write("Enter New Password: ");
                        newPass = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newPass) && newPass.Length >= 4)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Password must be at least 4 characters!");
                        Console.ResetColor();
                    }

                    string update = "UPDATE Teachers SET Password=@p WHERE ID=@id";
                    MySqlCommand up = new MySqlCommand(update, conn);
                    up.Parameters.AddWithValue("@p", newPass);
                    up.Parameters.AddWithValue("@id", id);
                    up.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Password Set Successfully!");
                    Console.ResetColor();

                    TeacherPanel(id, name);
                    return;
                }

                // ================= NORMAL LOGIN =================
                int attempts = 3;

                while (attempts > 0)
                {
                    Console.Write("\nEnter Password: ");
                    string pass = Console.ReadLine();

                    if (pass == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Login Successful!");
                        Console.ResetColor();

                        TeacherPanel(id, name);
                        return;
                    }

                    attempts--;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Wrong Password! Attempts left: {attempts}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n🚫 Too many failed attempts!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        static void TeacherPanel(int teacherId, string teacherName)
        {
            while (true)
            {
                Console.Clear();

                // ===== HEADER =====
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine($"      TEACHER DASHBOARD - {teacherName}");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                // ===== MENU =====
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Upload Assignment");
                Console.WriteLine("2. Upload Quiz");
                Console.WriteLine("3. View Assignments");
                Console.WriteLine("4. View Quizzes");
                Console.WriteLine("5. Logout");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                string ch = Console.ReadLine();

                Console.Clear();

                switch (ch)
                {
                    case "1":
                        UploadAssignment(teacherId);
                        break;

                    case "2":
                        UploadQuiz(teacherId);
                        break;

                    case "3":
                        ViewAssignments(teacherId);
                        break;

                    case "4":
                        ViewQuizzes(teacherId);
                        break;

                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("👋 Logging out... Goodbye!");
                        Console.ResetColor();

                        Console.WriteLine("\nPress any key...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid choice! Please select 1-6.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void UploadAssignment(int teacherId)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========= UPLOAD ASSIGNMENT =========\n");
            Console.ResetColor();

            Console.Write("Title: ");
            string title = Console.ReadLine();

            int marks;

            while (true)
            {
                Console.Write("Total Marks: ");
                if (int.TryParse(Console.ReadLine(), out marks) && marks > 0)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid marks! Enter a valid number.");
                Console.ResetColor();
            }

            string desc = "";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEnter Questions (type 'done' to finish):");
            Console.ResetColor();

            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                    break;

                if (!string.IsNullOrWhiteSpace(input))
                    desc += "- " + input + "\n";
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = @"INSERT INTO Assignments 
        (TeacherID, Title, Description, TotalMarks)
        VALUES (@t, @ti, @d, @m)";

                MySqlCommand cmd = new MySqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@t", teacherId);
                cmd.Parameters.AddWithValue("@ti", title);
                cmd.Parameters.AddWithValue("@d", desc);
                cmd.Parameters.AddWithValue("@m", marks);

                cmd.ExecuteNonQuery();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Assignment Added Successfully!");
            Console.ResetColor();

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
        static void ViewAssignments(int teacherId)
        {
            while (true) // 🔁 refresh loop
            {
                Console.Clear();

                // ===== HEADER =====
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine("     ASSIGNMENTS + SUBMISSIONS");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    // 🔥 FIX: sub.ID added
                    string q = @"
SELECT sub.ID AS SubID, a.ID AS AID, a.Title, a.Description, a.TotalMarks,
       s.Name AS StudentName,
       sub.SubmissionText,
       sub.MarksObtained
FROM Assignments a
LEFT JOIN AssignmentSubmissions sub ON a.ID = sub.AssignmentID
LEFT JOIN Students s ON s.ID = sub.StudentID
WHERE a.TeacherID = @t
ORDER BY a.ID";

                    MySqlCommand cmd = new MySqlCommand(q, conn);
                    cmd.Parameters.AddWithValue("@t", teacherId);

                    var r = cmd.ExecuteReader();

                    int currentAssignment = -1;
                    bool found = false;

                    while (r.Read())
                    {
                        found = true;

                        int aid = Convert.ToInt32(r["AID"]);

                        // ===== NEW ASSIGNMENT HEADER =====
                        if (aid != currentAssignment)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n📌 Assignment ID: {aid}");
                            Console.WriteLine($"Title       : {r["Title"]}");
                            Console.WriteLine($"Question    : {r["Description"]}");
                            Console.WriteLine($"Total Marks : {r["TotalMarks"]}");
                            Console.ResetColor();

                            Console.WriteLine("\n--- Submissions ---");
                            currentAssignment = aid;
                        }

                        // ===== SUBMISSIONS =====
                        if (r["StudentName"] != DBNull.Value)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\nSubmission ID: {r["SubID"]}");
                            Console.ResetColor();

                            Console.WriteLine($"👨‍🎓 Student   : {r["StudentName"]}");
                            Console.WriteLine($"📝 Answer    : {r["SubmissionText"]}");
                            Console.WriteLine($"🎯 Marks     : {r["MarksObtained"]}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("No submissions yet.");
                            Console.ResetColor();
                        }
                    }

                    r.Close();

                    // ===== NO DATA =====
                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ No assignments found!");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }

                    // ===== EDIT OPTION =====
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n=======================================");
                    Console.WriteLine("Enter Submission ID to edit marks");
                    Console.WriteLine("Press 0 to go back");
                    Console.WriteLine("=======================================");
                    Console.ResetColor();

                    int subId;
                    Console.Write("\nEnter ID: ");

                    if (!int.TryParse(Console.ReadLine(), out subId) || subId < 0)
                    {
                        Console.WriteLine("❌ Invalid input!");
                        Console.ReadKey();
                        continue;
                    }

                    if (subId == 0)
                        return;

                    // ===== GET TOTAL MARKS =====
                    string getMarksQ = @"
SELECT a.TotalMarks 
FROM AssignmentSubmissions sub
JOIN Assignments a ON a.ID = sub.AssignmentID
WHERE sub.ID = @id";

                    MySqlCommand getCmd = new MySqlCommand(getMarksQ, conn);
                    getCmd.Parameters.AddWithValue("@id", subId);

                    var totalObj = getCmd.ExecuteScalar();

                    if (totalObj == null)
                    {
                        Console.WriteLine("❌ Submission not found!");
                        Console.ReadKey();
                        continue;
                    }

                    int totalMarks = Convert.ToInt32(totalObj);

                    // ===== ENTER NEW MARKS =====
                    int newMarks;

                    while (true)
                    {
                        Console.Write($"Enter Marks (0 - {totalMarks}): ");

                        if (int.TryParse(Console.ReadLine(), out newMarks) &&
                            newMarks >= 0 && newMarks <= totalMarks)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid marks!");
                        Console.ResetColor();
                    }

                    // ===== UPDATE =====
                    string updateQ = "UPDATE AssignmentSubmissions SET MarksObtained=@m WHERE ID=@id";
                    MySqlCommand updateCmd = new MySqlCommand(updateQ, conn);

                    updateCmd.Parameters.AddWithValue("@m", newMarks);
                    updateCmd.Parameters.AddWithValue("@id", subId);

                    updateCmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Marks updated successfully!");
                    Console.ResetColor();

                    Console.WriteLine("\nPress any key to refresh...");
                    Console.ReadKey();
                }
            }
        }
        static void UploadQuiz(int teacherId)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================");
            Console.WriteLine("          UPLOAD QUIZ");
            Console.WriteLine("===================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n--- NEW QUESTION ---");
                    Console.ResetColor();

                    Console.Write("Enter Question: ");
                    string question = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(question))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Question cannot be empty!");
                        Console.ResetColor();
                        continue;
                    }

                    Console.Write("Is this MCQ? (yes/no): ");
                    string type = Console.ReadLine().ToLower();

                    string optionA = "", optionB = "", optionC = "", optionD = "";
                    string correctAnswer = "";
                    bool isMCQ = false;

                    if (type == "yes")
                    {
                        isMCQ = true;

                        Console.Write("Option A: ");
                        optionA = Console.ReadLine();

                        Console.Write("Option B: ");
                        optionB = Console.ReadLine();

                        Console.Write("Option C: ");
                        optionC = Console.ReadLine();

                        Console.Write("Option D: ");
                        optionD = Console.ReadLine();

                        while (true)
                        {
                            Console.Write("Correct Answer (A/B/C/D): ");
                            string ans = Console.ReadLine().ToUpper();

                            if (ans == "A" || ans == "B" || ans == "C" || ans == "D")
                            {
                                correctAnswer = ans;
                                break;
                            }

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Invalid option! Choose A, B, C or D.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write("Enter Expected Answer: ");
                        correctAnswer = Console.ReadLine();
                    }

                    // ===== MARKS VALIDATION =====
                    int marks;

                    while (true)
                    {
                        Console.Write("Enter Total Marks: ");
                        if (int.TryParse(Console.ReadLine(), out marks) && marks > 0)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid marks! Enter a valid number.");
                        Console.ResetColor();
                    }

                    // ===== DATABASE INSERT =====
                    string query = @"INSERT INTO Quizzes 
            (TeacherID, Question, OptionA, OptionB, OptionC, OptionD, CorrectAnswer, IsMCQ, TotalMarks)
            VALUES (@TeacherID, @Question, @A, @B, @C, @D, @Correct, @IsMCQ, @Marks)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    cmd.Parameters.AddWithValue("@Question", question);
                    cmd.Parameters.AddWithValue("@A", optionA);
                    cmd.Parameters.AddWithValue("@B", optionB);
                    cmd.Parameters.AddWithValue("@C", optionC);
                    cmd.Parameters.AddWithValue("@D", optionD);
                    cmd.Parameters.AddWithValue("@Correct", correctAnswer);
                    cmd.Parameters.AddWithValue("@IsMCQ", isMCQ);
                    cmd.Parameters.AddWithValue("@Marks", marks);

                    cmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Quiz added successfully!");
                    Console.ResetColor();

                    Console.Write("\n➕ Add another question? (yes/no): ");
                    string again = Console.ReadLine().ToLower();

                    if (again != "yes")
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎯 Quiz Upload Completed!");
            Console.ResetColor();

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static void ViewQuizzes(int teacherId)
        {
            while (true) // 🔁 refresh loop
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine("        QUIZZES + RESULTS");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    // 🔥 FIX: sub.ID included (important)
                    string q = @"
SELECT sub.ID AS SubID, q.ID AS QID, q.Question, q.TotalMarks,
       s.Name AS StudentName,
       sub.Answer,
       sub.MarksObtained
FROM Quizzes q
LEFT JOIN QuizSubmissions sub ON q.ID = sub.QuizID
LEFT JOIN Students s ON s.ID = sub.StudentID
WHERE q.TeacherID = @t
ORDER BY q.ID";

                    MySqlCommand cmd = new MySqlCommand(q, conn);
                    cmd.Parameters.AddWithValue("@t", teacherId);

                    var r = cmd.ExecuteReader();

                    int currentQuiz = -1;
                    bool found = false;

                    while (r.Read())
                    {
                        found = true;

                        int qid = Convert.ToInt32(r["QID"]);

                        // ===== NEW QUIZ HEADER =====
                        if (qid != currentQuiz)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n📌 Quiz ID: {qid}");
                            Console.WriteLine($"Question   : {r["Question"]}");
                            Console.WriteLine($"Total Marks: {r["TotalMarks"]}");
                            Console.ResetColor();

                            Console.WriteLine("\n--- Student Attempts ---");
                            currentQuiz = qid;
                        }

                        // ===== ATTEMPTS =====
                        if (r["StudentName"] != DBNull.Value)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\nSubmission ID: {r["SubID"]}");
                            Console.ResetColor();

                            Console.WriteLine($"👨‍🎓 Student: {r["StudentName"]}");
                            Console.WriteLine($"📝 Answer: {r["Answer"]}");
                            Console.WriteLine($"🎯 Marks: {r["MarksObtained"]}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("No attempts yet.");
                            Console.ResetColor();
                        }
                    }

                    r.Close();

                    // ===== NO DATA =====
                    if (!found)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ No quizzes found!");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }

                    // ===== EDIT OPTION =====
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n=======================================");
                    Console.WriteLine("Enter Submission ID to edit marks");
                    Console.WriteLine("Press 0 to go back");
                    Console.WriteLine("=======================================");
                    Console.ResetColor();

                    int subId;
                    Console.Write("\nEnter ID: ");

                    if (!int.TryParse(Console.ReadLine(), out subId) || subId < 0)
                    {
                        Console.WriteLine("❌ Invalid input!");
                        Console.ReadKey();
                        continue;
                    }

                    if (subId == 0)
                        return;

                    // ===== GET TOTAL MARKS =====
                    string getMarksQ = @"
SELECT q.TotalMarks 
FROM QuizSubmissions sub
JOIN Quizzes q ON q.ID = sub.QuizID
WHERE sub.ID = @id";

                    MySqlCommand getCmd = new MySqlCommand(getMarksQ, conn);
                    getCmd.Parameters.AddWithValue("@id", subId);

                    var totalObj = getCmd.ExecuteScalar();

                    if (totalObj == null)
                    {
                        Console.WriteLine("❌ Submission not found!");
                        Console.ReadKey();
                        continue;
                    }

                    int totalMarks = Convert.ToInt32(totalObj);

                    // ===== ENTER MARKS =====
                    int newMarks;

                    while (true)
                    {
                        Console.Write($"Enter Marks (0 - {totalMarks}): ");

                        if (int.TryParse(Console.ReadLine(), out newMarks) &&
                            newMarks >= 0 && newMarks <= totalMarks)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid marks!");
                        Console.ResetColor();
                    }

                    // ===== UPDATE =====
                    string updateQ = "UPDATE QuizSubmissions SET MarksObtained=@m WHERE ID=@id";
                    MySqlCommand updateCmd = new MySqlCommand(updateQ, conn);

                    updateCmd.Parameters.AddWithValue("@m", newMarks);
                    updateCmd.Parameters.AddWithValue("@id", subId);

                    updateCmd.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Marks updated successfully!");
                    Console.ResetColor();

                    Console.WriteLine("\nPress any key to refresh...");
                    Console.ReadKey();
                }
            }
        }

        // ================= STUDENT LOGIN & PANEL =================
        static void StudentLogin()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================");
            Console.WriteLine("         STUDENT LOGIN");
            Console.WriteLine("===================================\n");
            Console.ResetColor();

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string q = "SELECT * FROM Students WHERE Username=@u";
                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@u", username);

                var r = cmd.ExecuteReader();

                if (!r.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ User not found!");
                    Console.ResetColor();

                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                    return;
                }

                int id = Convert.ToInt32(r["ID"]);
                string name = r["Name"].ToString();
                string password = r["Password"].ToString();

                r.Close();

                // ===== FIRST LOGIN =====
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n🔐 First Login - Set Password");
                    Console.ResetColor();

                    string newPass;

                    while (true)
                    {
                        Console.Write("Enter New Password: ");
                        newPass = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newPass) && newPass.Length >= 4)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Password must be at least 4 characters!");
                        Console.ResetColor();
                    }

                    string update = "UPDATE Students SET Password=@p WHERE ID=@id";
                    MySqlCommand up = new MySqlCommand(update, conn);
                    up.Parameters.AddWithValue("@p", newPass);
                    up.Parameters.AddWithValue("@id", id);
                    up.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Password Set Successfully!");
                    Console.ResetColor();

                    StudentPanel(id, name);
                    return;
                }

                // ===== NORMAL LOGIN =====
                int attempts = 3;

                while (attempts > 0)
                {
                    Console.Write("\nEnter Password: ");
                    string pass = Console.ReadLine();

                    if (pass == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Login Successful!");
                        Console.ResetColor();

                        Console.WriteLine("\nPress any key...");
                        Console.ReadKey();

                        StudentPanel(id, name);
                        return;
                    }

                    attempts--;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Wrong Password! Attempts left: {attempts}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n🚫 Too many failed attempts!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static void StudentPanel(int studentId, string studentName)
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine($"     STUDENT DASHBOARD - {studentName}");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Submit Assignment");
                Console.WriteLine("2. Solve Quiz");
                Console.WriteLine("3. View Marks");
                Console.WriteLine("4. Logout");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        SubmitAssignment(studentId);
                        break;

                    case "2":
                        SolveQuiz(studentId);
                        break;

                    case "3":
                        ViewMarks(studentId);
                        break;

                    case "4":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("👋 Logging out... Goodbye!");
                        Console.ResetColor();

                        Console.WriteLine("\nPress any key...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid choice! Please select 1-4.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void SubmitAssignment(int studentId)
        {
            Console.Clear();

            // ===== HEADER =====
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("         SUBMIT ASSIGNMENT");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ❌ FIX: already submitted assignments exclude
                string q = @"
            SELECT a.ID, a.Title, a.Description
            FROM Assignments a
            JOIN StudentTeacher st ON st.TeacherID = a.TeacherID
            WHERE st.StudentID = @sid
            AND NOT EXISTS (
                SELECT 1 FROM AssignmentSubmissions sub
                WHERE sub.StudentID = @sid
                AND sub.AssignmentID = a.ID
            )";

                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);

                var r = cmd.ExecuteReader();

                bool found = false;
                List<int> ids = new List<int>();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Available Assignments:\n");
                Console.ResetColor();

                while (r.Read())
                {
                    found = true;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"ID: {r["ID"]}");
                    Console.ResetColor();

                    Console.WriteLine($"Title    : {r["Title"]}");
                    Console.WriteLine($"Question : {r["Description"]}");
                    Console.WriteLine("-----------------------------------");

                    ids.Add(Convert.ToInt32(r["ID"]));
                }

                r.Close();

                // ===== NO ASSIGNMENT =====
                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n🎉 No pending assignments! All submitted.");
                    Console.ResetColor();

                    Console.ReadKey();
                    return;
                }

                // ===== VALID ID =====
                int aid;

                while (true)
                {
                    Console.Write("\nEnter Assignment ID (or 0 to cancel): ");

                    if (int.TryParse(Console.ReadLine(), out aid))
                    {
                        if (aid == 0)
                        {
                            Console.WriteLine("Submission cancelled.");
                            Console.ReadKey();
                            return;
                        }

                        if (ids.Contains(aid))
                            break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid Assignment ID!");
                    Console.ResetColor();
                }

                // ===== SUBMISSION TEXT =====
                string text;

                while (true)
                {
                    Console.Write("Enter your submission: ");
                    text = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(text))
                        break;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Submission cannot be empty!");
                    Console.ResetColor();
                }

                // ===== INSERT =====
                string insert = @"INSERT INTO AssignmentSubmissions
                         (StudentID, AssignmentID, SubmissionText)
                         VALUES (@s, @a, @t)";

                MySqlCommand cmd2 = new MySqlCommand(insert, conn);
                cmd2.Parameters.AddWithValue("@s", studentId);
                cmd2.Parameters.AddWithValue("@a", aid);
                cmd2.Parameters.AddWithValue("@t", text);

                cmd2.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Assignment submitted successfully!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static void SolveQuiz(int studentId)
        {
            Console.Clear();

            // ===== HEADER =====
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("              QUIZ SECTION");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // ❌ FIX: already attempted quizzes exclude
                string q = @"
            SELECT q.* 
            FROM Quizzes q
            JOIN StudentTeacher st ON st.TeacherID = q.TeacherID
            WHERE st.StudentID = @sid
            AND NOT EXISTS (
                SELECT 1 
                FROM QuizSubmissions qs
                WHERE qs.StudentID = @sid
                AND qs.QuizID = q.ID
            )";

                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);

                var r = cmd.ExecuteReader();

                List<Dictionary<string, object>> quizzes = new List<Dictionary<string, object>>();

                while (r.Read())
                {
                    quizzes.Add(new Dictionary<string, object>
                    {
                        ["ID"] = r["ID"],
                        ["Question"] = r["Question"],
                        ["A"] = r["OptionA"],
                        ["B"] = r["OptionB"],
                        ["C"] = r["OptionC"],
                        ["D"] = r["OptionD"],
                        ["Correct"] = r["CorrectAnswer"],
                        ["IsMCQ"] = r["IsMCQ"],
                        ["Marks"] = r["TotalMarks"]
                    });
                }

                r.Close();

                // ===== NO QUIZ =====
                if (quizzes.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("🎉 No pending quizzes! You have completed all.");
                    Console.ResetColor();

                    Console.ReadKey();
                    return;
                }

                // ===== QUIZ LOOP =====
                foreach (var qz in quizzes)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Question ID: {qz["ID"]}\n");
                    Console.ResetColor();

                    Console.WriteLine(qz["Question"]);

                    string answer = "";
                    int marks = 0;

                    // ===== MCQ =====
                    if (Convert.ToBoolean(qz["IsMCQ"]))
                    {
                        Console.WriteLine($"\nA) {qz["A"]}");
                        Console.WriteLine($"B) {qz["B"]}");
                        Console.WriteLine($"C) {qz["C"]}");
                        Console.WriteLine($"D) {qz["D"]}");

                        while (true)
                        {
                            Console.Write("\nEnter (A/B/C/D or 0 to cancel): ");
                            answer = Console.ReadLine().ToUpper();

                            if (answer == "0")
                            {
                                Console.WriteLine("Quiz cancelled.");
                                Console.ReadKey();
                                return;
                            }

                            if (answer == "A" || answer == "B" || answer == "C" || answer == "D")
                                break;

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Invalid option!");
                            Console.ResetColor();
                        }

                        marks = (answer == qz["Correct"].ToString())
                                ? Convert.ToInt32(qz["Marks"])
                                : 0;
                    }
                    else
                    {
                        // ===== SHORT ANSWER =====
                        while (true)
                        {
                            Console.Write("\nEnter answer (or 0 to cancel): ");
                            answer = Console.ReadLine();

                            if (answer == "0")
                            {
                                Console.WriteLine("Quiz cancelled.");
                                Console.ReadKey();
                                return;
                            }

                            if (!string.IsNullOrWhiteSpace(answer))
                                break;

                            Console.WriteLine("❌ Answer cannot be empty!");
                        }

                        marks = -1;
                    }

                    // ===== INSERT =====
                    string insert = @"INSERT INTO QuizSubmissions
                             (StudentID, QuizID, Answer, MarksObtained)
                             VALUES (@s, @q, @a, @m)";

                    MySqlCommand cmd2 = new MySqlCommand(insert, conn);
                    cmd2.Parameters.AddWithValue("@s", studentId);
                    cmd2.Parameters.AddWithValue("@q", qz["ID"]);
                    cmd2.Parameters.AddWithValue("@a", answer);
                    cmd2.Parameters.AddWithValue("@m", marks);

                    cmd2.ExecuteNonQuery();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Answer submitted!");
                    Console.ResetColor();

                    Console.ReadKey();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎉 Quiz completed!");
            Console.ResetColor();

            Console.ReadKey();
        }
        static void ViewMarks(int studentId)
        {
            Console.Clear();

            // ===== HEADER =====
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("              YOUR MARKS");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                bool hasData = false;

                // ================= ASSIGNMENT MARKS =================
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("📘 ASSIGNMENT MARKS\n");
                Console.ResetColor();

                string q1 = @"SELECT a.Title, sub.MarksObtained 
                      FROM AssignmentSubmissions sub
                      JOIN Assignments a ON a.ID=sub.AssignmentID
                      WHERE sub.StudentID=@id";

                MySqlCommand cmd1 = new MySqlCommand(q1, conn);
                cmd1.Parameters.AddWithValue("@id", studentId);

                var r1 = cmd1.ExecuteReader();

                while (r1.Read())
                {
                    hasData = true;

                    Console.WriteLine($"📌 {r1["Title"]}");
                    Console.WriteLine($"   Marks: {r1["MarksObtained"]}");
                    Console.WriteLine("----------------------------------");
                }

                r1.Close();

                // ================= QUIZ MARKS =================
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n📝 QUIZ MARKS\n");
                Console.ResetColor();

                string q2 = @"SELECT q.Question, sub.MarksObtained 
                      FROM QuizSubmissions sub
                      JOIN Quizzes q ON q.ID=sub.QuizID
                      WHERE sub.StudentID=@id";

                MySqlCommand cmd2 = new MySqlCommand(q2, conn);
                cmd2.Parameters.AddWithValue("@id", studentId);

                var r2 = cmd2.ExecuteReader();

                while (r2.Read())
                {
                    hasData = true;

                    Console.WriteLine($"📌 {r2["Question"]}");
                    Console.WriteLine($"   Marks: {r2["MarksObtained"]}");
                    Console.WriteLine("----------------------------------");
                }

                r2.Close();

                // ================= NO DATA CASE =================
                if (!hasData)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ No marks available yet!");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}