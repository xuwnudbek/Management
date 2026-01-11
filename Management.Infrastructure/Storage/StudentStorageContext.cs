using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Management.Application.Interfaces;
using Management.Domain.Models;

namespace Management.Infrastructure.Data;

public class StudentStorageContext : IStudentStorageContext
{
    private readonly string _dbFileAbsolutePath;

    public StudentStorageContext()
    {
        _dbFileAbsolutePath = Path.Combine("DB", "Students.json");

        try
        {
            // if (!Directory.Exists(DirPath))
            //     Directory.CreateDirectory(DirPath);

            if (!File.Exists(_dbFileAbsolutePath))
                File.Create(_dbFileAbsolutePath).Close();
        }
        catch (Exception exc)
        {
            Console.WriteLine($"Error: {exc.Message}");
        }
    }

    public List<Student> Load()
    {
        List<Student> students = [];

        try
        {
            var content = File.ReadAllText(_dbFileAbsolutePath);

            students = JsonSerializer.Deserialize<List<Student>>(content);
        }
        catch (Exception exc)
        {
            Console.WriteLine($"Error: {exc.Message}");
        }

        return students;
    }

    public void Save(List<Student> students)
    {
        try
        {
            File.WriteAllText(_dbFileAbsolutePath, JsonSerializer.Serialize(students));
        }
        catch (Exception exc)
        {
            Console.WriteLine($"Error: {exc.Message}");
        }
    }
}
