using System;
using System.Collections.Generic;
using System.Linq;
using Management.Application.Services;
using Management.Domain.Models;

namespace Management.Client;

public class Program
{
    private static readonly StudentService studentService = new();

    // Show All Student Method
    public static void MenuShowAllStudents()
    {
        Console.Clear();
        Console.WriteLine("========== Show all students ==========");

        List<Student> students = studentService.GetAllStudents();

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
        }
    }

    // Show student by Id
    public static void MenuShowStudentById()
    {
        Console.Clear();
        Console.WriteLine($"========== Show students by Id ==========");

        Console.Write("Student Id: ");
        string studentId = Console.ReadLine();

        Student student = studentService.GetStudentById(studentId);

        if (student is null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Student not found!");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
    }

    // Menu Search student
    public static void MenuSearchStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Search students by text ==========");

        Console.Write("Search Text: ");
        string searchText = Console.ReadLine();

        List<Student> students = studentService.SearchByText(searchText);

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
        }
    }

    // Menu Add Student
    public static void MenuAddStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Add student ==========");

        // New Student Data
        Console.Write("FirstName: ");
        string firstName = Console.ReadLine();

        Console.Write("LastName: ");
        string lastName = Console.ReadLine();

        studentService.Add(firstName, lastName);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Student successfully added!");
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Menu Update Student
    public static void MenuUpdateStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Search students by text ==========");

        Console.Write("Student Id: ");
        string studentId = Console.ReadLine();

        // New Student Data
        Console.Write("FirstName: ");
        string newFirstName = Console.ReadLine();
        Console.Write("LastName: ");
        string newLastName = Console.ReadLine();

        Student newStudent = new(studentId, newFirstName, newLastName);

        bool hasUpdated = studentService.Update(newStudent);

        if (hasUpdated)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Student successfully updated!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Student update failed.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    // Menu Delete Student
    public static void MenuDeleteStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Delete student ==========");

        Console.Write("Student Id: ");
        string studentId = Console.ReadLine();

        bool deleteSucceeded = studentService.DeleteStudentById(studentId);

        if (deleteSucceeded)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student deleted successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Student deleted failed.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static void Main(string[] args)
    {
        // Configure Console
        Console.Clear();
        Console.Title = "Management";

        // Main Menu
        Console.WriteLine("====================Welcome To Management App====================\n");

        Console.WriteLine("Select an option:");
        Console.WriteLine("  1. Show all students");
        Console.WriteLine("  2. Show student by Id");
        Console.WriteLine("  3. Search student");
        Console.WriteLine("  4. Add student");
        Console.WriteLine("  5. Update student");
        Console.WriteLine("  6. Delete student");
        Console.WriteLine("  0. Exit");

        // Read User Option
        Console.Write(">> ");
        string option = Console.ReadLine();

        // Navigate to selected option
        switch (option)
        {
            case "1":
                MenuShowAllStudents();
                break;
            case "2":
                MenuShowStudentById();
                break;
            case "3":
                MenuSearchStudent();
                break;
            case "4":
                MenuAddStudent();
                break;
            case "5":
                MenuUpdateStudent();
                break;
            case "6":
                MenuDeleteStudent();
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }

        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();

        Main(args);
    }
}
