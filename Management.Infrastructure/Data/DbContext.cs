using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Management.Domain.Models;

namespace Management.Infrastructure.Data;

public class DbContext
{
    private List<Student> _students;
    public List<Student> Students
    {
        get => _students;
        set
        {
            _students = value;
            WriteAllStudentsToFile(value);
        }
    }

    private readonly string _dirPath = "DB";
    private readonly string _filePath = "db.json";

    private readonly string _dbFileAbsolutePath;

    public DbContext()
    {
        _dbFileAbsolutePath = Path.Combine(_dirPath, _filePath);

        try
        {
            if (!Directory.Exists(_dirPath))
                Directory.CreateDirectory(_dirPath);

            if (!File.Exists(_dbFileAbsolutePath))
            {
                File.Create(_dbFileAbsolutePath).Close();
                File.WriteAllText(_dbFileAbsolutePath, Environment.NewLine);
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"Error: {exc.Message}");
        }

        this.Students = GetAllStudentsFromFile();
    }

    private List<Student> GetAllStudentsFromFile()
    {
        List<Student> students = [];

        try
        {
            string data = File.ReadAllText(_dbFileAbsolutePath);

            students = JsonSerializer.Deserialize<List<Student>>(data);
        }
        catch (Exception exc)
        {
            Console.WriteLine($"Error: {exc.Message}");
        }

        return students;
    }

    private void WriteAllStudentsToFile(List<Student> students)
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
