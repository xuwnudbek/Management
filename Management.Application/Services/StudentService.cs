using System;
using System.Collections.Generic;
using System.Linq;
using Management.Domain.Models;
using Management.Infrastructure.Data;

namespace Management.Application.Services;

public class StudentService
{
    public DbContext DbContext { get; set; }

    public StudentService()
    {
        this.DbContext = new DbContext();
    }

    public void Add(string firstName, string lastName)
    {
        Student student = new Student(new Random().Next(1, 1000).ToString(), firstName, lastName);

        this.DbContext.Students.Add(student);
    }

    public List<Student> GetAllStudents()
    {
        return this.DbContext.Students;
    }

    public Student GetStudentById(string id)
    {
        Student foundStudent = this.DbContext.Students.FirstOrDefault(
            (student) => student.Id == id
        );

        return foundStudent;
    }

    public List<Student> SearchByText(string text)
    {
        List<Student> filteredStudents = this
            .DbContext.Students.Where(
                (student) =>
                    student.FirstName.Contains(text, StringComparison.CurrentCultureIgnoreCase)
                    || student.LastName.Contains(text, StringComparison.CurrentCultureIgnoreCase)
            )
            .ToList();

        return filteredStudents;
    }

    public bool Update(Student student)
    {
        Student foundStudent = this.DbContext.Students.FirstOrDefault((s) => s.Id == student.Id);

        if (foundStudent is null)
        {
            return false;
        }

        foundStudent.FirstName = student.FirstName;
        foundStudent.LastName = student.LastName;

        return true;
    }

    public bool DeleteStudentById(string id)
    {
        Student foundStudent = this.DbContext.Students.FirstOrDefault((s) => s.Id == id);

        if (foundStudent is null)
        {
            return false;
        }

        bool hasRemoved = this.DbContext.Students.Remove(foundStudent);

        return hasRemoved;
    }
}
