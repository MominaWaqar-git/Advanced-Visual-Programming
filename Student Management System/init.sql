CREATE DATABASE IF NOT EXISTS studentdb;
USE studentdb;

-- ================= STUDENTS =================
CREATE TABLE Students (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Age INT,
    CNIC VARCHAR(13),
    Address VARCHAR(200),
    Phone VARCHAR(11),
    Email VARCHAR(100),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255),
    Subject VARCHAR(100)
);

-- ================= TEACHERS =================
CREATE TABLE Teachers (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Age INT,
    CNIC VARCHAR(13),
    Address VARCHAR(200),
    Phone VARCHAR(11),
    Email VARCHAR(100),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255),
    Subject VARCHAR(100)
);

-- ================= STUDENT-TEACHER RELATION =================
CREATE TABLE StudentTeacher (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    TeacherID INT,
    FOREIGN KEY (StudentID) REFERENCES Students(ID) ON DELETE CASCADE,
    FOREIGN KEY (TeacherID) REFERENCES Teachers(ID) ON DELETE CASCADE
);

-- ================= ASSIGNMENTS =================
CREATE TABLE Assignments (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    TeacherID INT,
    Title VARCHAR(200),
    Description TEXT,
    TotalMarks INT,
    FOREIGN KEY (TeacherID) REFERENCES Teachers(ID)
);

-- ================= ASSIGNMENT SUBMISSIONS =================
CREATE TABLE AssignmentSubmissions (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    AssignmentID INT,
    SubmissionText TEXT,
    MarksObtained INT DEFAULT -1,
    FOREIGN KEY (StudentID) REFERENCES Students(ID),
    FOREIGN KEY (AssignmentID) REFERENCES Assignments(ID)
);

-- ================= QUIZZES =================

CREATE TABLE Quizzes (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    TeacherID INT,
    Question TEXT,
    OptionA VARCHAR(100),
    OptionB VARCHAR(100),
    OptionC VARCHAR(100),
    OptionD VARCHAR(100),
    CorrectAnswer VARCHAR(50),
    IsMCQ TINYINT(1),
    TotalMarks INT,
    FOREIGN KEY (TeacherID) REFERENCES Teachers(ID)
);

-- ================= QUIZ SUBMISSIONS =================
CREATE TABLE QuizSubmissions (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    QuizID INT,
    Answer TEXT,
    MarksObtained INT DEFAULT -1,
    FOREIGN KEY (StudentID) REFERENCES Students(ID),
    FOREIGN KEY (QuizID) REFERENCES Quizzes(ID)
);