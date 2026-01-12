using System;
using System.Collections.Generic;
using Management.Application.Services;
using Management.Domain.Models;
using Management.Infrastructure.Brokers;
using Management.Infrastructure.Data;
using Management.Infrastructure.Repositories;

namespace Management.Client;

public class Program
{
    private static StudentService _studentService;

    // Show All Student
    private static void MenuShowAllStudents()
    {
        Console.Clear();
        Console.WriteLine("========== Show all students ==========");

        List<Student> students = _studentService.GetAll();

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
        }
    }

    // Show student by Id
    private static void MenuShowStudentById()
    {
        Console.Clear();
        Console.WriteLine($"========== Show students by Id ==========");

        Console.Write("Student Id: ");
        string studentId = Console.ReadLine();

        Student student = _studentService.GetById(studentId);

        if (student is null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Student not found!");

            SetConsoleDefaultColor();

            return;
        }

        Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
    }

    // Menu Search student
    private static void MenuSearchStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Search students by text ==========");

        Console.Write("Search Text: ");
        string searchText = Console.ReadLine();

        var matchedStudents = _studentService.Search(searchText);

        foreach (var student in matchedStudents)
        {
            Console.WriteLine($"{student.Id} - {student.FirstName + " " + student.LastName}");
        }
    }

    // Menu Add Student
    private static void MenuAddStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Add student ==========");

        // New Student Data
        Console.Write("FirstName: ");
        string firstName = Console.ReadLine();

        Console.Write("LastName: ");
        string lastName = Console.ReadLine();

        _studentService.Add(firstName, lastName);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Student successfully added!");

        SetConsoleDefaultColor();
    }

    // Menu Update Student
    private static void MenuUpdateStudent()
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

        bool hasUpdated = _studentService.Update(newStudent);

        if (hasUpdated)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Student successfully updated!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Student update failed.");
        }

        SetConsoleDefaultColor();
    }

    // Menu Delete Student
    private static void MenuDeleteStudent()
    {
        Console.Clear();
        Console.WriteLine($"========== Delete student ==========");

        Console.Write("Student Id: ");
        var studentId = Console.ReadLine();

        var deleteSucceeded = _studentService.Delete(studentId);

        if (deleteSucceeded)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student deleted successfully.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Student deleted failed.");
        }

        SetConsoleDefaultColor();
    }

    // Menu Export/Import students from/to Excel
    private static void MenuExportImportStudents()
    {
        Console.Clear();
        Console.WriteLine($"========== Export/Import students ==========");

        Console.WriteLine("1. Back to Home");
        Console.WriteLine("2. Export to Excel");
        Console.WriteLine("3. Import from Excel");

        Console.Write("\n>> ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                MenuMain();
                break;

            case "2":
                _studentService.ExportToExcel();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Students exported successfully to Excel.");
                SetConsoleDefaultColor();
                break;

            case "3":
                Console.Write("File path: ");
                var filePath = Console.ReadLine();

                var countOfImportedStudents = _studentService.ImportFromExcel(filePath);

                if (countOfImportedStudents is null)
                    break;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"Students imported successfully from Excel.\nCount of imported students: {countOfImportedStudents}"
                );

                SetConsoleDefaultColor();
                break;

            default:
                MenuExportImportStudents();
                break;
        }
    }

    private static void SetConsoleDefaultColor()
    {
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void MenuMain()
    {
        var keepRepeating = true;

        while (keepRepeating)
        {
            // Configure Console
            Console.Clear();
            Console.Title = "Management";

            // Main Menu
            Console.WriteLine(
                "====================Welcome To Management App====================\n"
            );

            Console.WriteLine("Select an option:");
            Console.WriteLine("  1. Show all students");
            Console.WriteLine("  2. Show student by Id");
            Console.WriteLine("  3. Search student");
            Console.WriteLine("  4. Add student");
            Console.WriteLine("  5. Update student");
            Console.WriteLine("  6. Delete student");
            Console.WriteLine("  7. Export/Import students");
            Console.WriteLine("  0. Exit");

            // Read User Option
            Console.Write(">> ");
            var option = Console.ReadLine();

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
                case "7":
                    MenuExportImportStudents();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine(
                "\nPress any key to return to the main menu & Escape to exit from the application..."
            );

            var consoleKey = Console.ReadKey();

            if (consoleKey.Key == ConsoleKey.Escape)
                keepRepeating = false;
        }
    }

    public static void Main(string[] args)
    {
        /*
        1. Application
            Services, Interfaces
        
        2. Domain
            Models

        3. Infrastructure
            Brokers, Repositories, Storage
        
        4. Client
            Program.cs
        */

        // Get instance of StudentStorageContext
        var studentStorageContext = new StudentStorageContext();

        // Get instance of StudentRepository
        var studentRepository = new StudentRepository(studentStorageContext);
        var studentExcelBroker = new StudentExcelBroker();

        // Get instance of StudentService
        _studentService = new StudentService(studentRepository, studentExcelBroker);

        // Start project's entry point
        MenuMain();
    }
}
