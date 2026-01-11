using System;
using System.Collections.Generic;
using System.Linq;
using Management.Application.Interfaces;
using Management.Domain.Models;

namespace Management.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IStudentStorageContext _context;
    private List<Student> Students { get; set; }

    public StudentRepository(IStudentStorageContext context)
    {
        this._context = context;
        this.Students = _context.Load();
    }

    public void Add(Student student)
    {
        Students.Add(student);
        _context.Save(Students);
    }

    public void AddAll(List<Student> students)
    {
        foreach (var student in students)
        {
            var existingStudent = Students.FirstOrDefault(s => s.Id == student.Id);

            if (existingStudent != null)
                continue;

            Students.Add(student);
        }

        _context.Save(Students);
    }

    public Student GetById(string id)
    {
        var student = Students.FirstOrDefault(student => student.Id == id);
        return student;
    }

    public List<Student> GetAll()
    {
        return Students;
    }

    public bool Update(Student student)
    {
        var targetStudent = Students.FirstOrDefault(s => s.Id == student.Id);

        if (targetStudent == null)
            return false;

        targetStudent = student;
        _context.Save(Students);

        return true;
    }

    public bool Remove(string id)
    {
        Students.RemoveAll(s => s.Id == id);
        _context.Save(Students);

        var hasRemoved = Students.All(s => s.Id != id);

        return hasRemoved;
    }

    public List<Student> Search(string text)
    {
        var result = Students
            .Where(s =>
                (s.FirstName + " " + s.LastName).Contains(text, StringComparison.OrdinalIgnoreCase)
            )
            .ToList();

        return result;
    }
}
