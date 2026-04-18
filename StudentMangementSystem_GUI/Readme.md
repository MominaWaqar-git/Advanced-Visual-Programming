# 🎓 Student Management System (C# + MySQL)

A console-based Student Management System developed using **C# (.NET)** and **MySQL database**.  
The system manages students, teachers, assignments, quizzes, and grading with a role-based structure.

---

## 📌 Features

### 👨‍🏫 Admin
- Add / View / Update / Delete Students
- Add / View / Update / Delete Teachers
- Assign Teachers to Students (Many-to-Many Relationship)

### 👨‍🏫 Teacher
- Secure Login System
- First-time password setup
- Upload Assignments
- Upload Quizzes (MCQs + Short Questions)
- View and grade student submissions

### 👨‍🎓 Student
- Secure Login System
- Submit Assignments
- Solve Quizzes
- View Marks and Results

---

## 🛠️ Technologies Used

- C# (.NET Console Application)
- MySQL Database (XAMPP / phpMyAdmin)
- Visual Studio

---

## 🗄️ Database Tables

- Students  
- Teachers  
- Assignments  
- AssignmentSubmissions  
- Quizzes  
- QuizSubmissions  
- StudentTeacher (Mapping Table)

---

## 🔗 System Workflow

1. Admin assigns teachers to students  
2. Teachers create assignments & quizzes  
3. Students submit assignments and solve quizzes  
4. Teachers evaluate and assign marks  
5. Students view their results  

---

## 📂 Project Structure
Student-Management-System/
│
├── Program.cs
├── Teacher Functions
├── Student Functions
├── Admin Functions
└── Database Scripts

---

## 🚀 How to Run

- Open XAMPP and start MySQL
- Open Visual Studio
- Add MySQL Connector (MySql.Data)
- Update DB connection string
- Run the project

---

## 📊 Key Highlights

- Role-based authentication system
- Secure login with password setup
- Many-to-many Student-Teacher mapping
- MCQ + Subjective quiz system
- Real-time marks evaluation
- Console-based UI with structured menus

---

## 📌 Future Improvements

- Web version using ASP.NET MVC
- GUI interface (Windows Forms / WPF)
- Email notifications
- Attendance system
- Dashboard analytics for admin

---

## 👨‍💻 Author

- Name: Momina Waqar
- Department: Computer Science  
- Project: Student Management System  

---

## 📃 Note

This project is created for educational purposes only.
```
