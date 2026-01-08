using System;
using System.Collections.Generic;
using System.Linq;
using Management.Application.Services;
using Management.Domain.Models;

namespace Management.Client;

public class Program
{
    public static void Main(string[] args)
    {
        var studentService = new StudentService();

        studentService.AddStudent("Xushnudbek", "Abdusamatov");
        studentService.AddStudent("Akbar", "Abdusamatov");
        studentService.AddStudent("Arslon", "Abdusamatov");
        studentService.AddStudent("Jonibek", "Abdusamatov");
        studentService.AddStudent("Mubashshir", "Abdusamatov");
        studentService.AddStudent("Diyorbek", "Abdusamatov");
        studentService.AddStudent("Sirtlonbek", "Abdusamatov");

        List<Student> students = studentService.GetStudents();

        // Clear terminal & Show all students
        Console.Clear();

        Console.WriteLine($"Students: {students.Count}");

        foreach (Student student in students)
        {
            Console.WriteLine($"{student.Id} : {student.FirstName + " " + student.LastName}");
        }
    }
}
