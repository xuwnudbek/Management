using System;
using System.Collections.Generic;
using Management.Application.Interfaces;
using Management.Domain.Models;

namespace Management.Application.Services;

public class StudentService
{
    private IStudentRepository StudentRepository { get; set; }
    private IStudentExcelBroker StudentExcelBroker { get; set; }

    public StudentService(
        IStudentRepository studentRepository,
        IStudentExcelBroker studentExcelBroker
    )
    {
        this.StudentRepository = studentRepository;
        this.StudentExcelBroker = studentExcelBroker;
    }

    public void Add(string firstName, string lastName)
    {
        if (firstName == null || lastName == null)
            return;

        var newStudent = new Student(
            $"ID{new Random().Next(1000, 10000).ToString()}",
            firstName,
            lastName
        );

        StudentRepository.Add(newStudent);
    }

    public Student GetById(string id)
    {
        return StudentRepository.GetById(id);
    }

    public List<Student> GetAll()
    {
        return StudentRepository.GetAll();
    }

    public bool Update(Student student)
    {
        bool hasUpdated = StudentRepository.Update(student);

        return hasUpdated;
    }

    public bool Delete(string id)
    {
        var hasRemoved = StudentRepository.Remove(id);

        return hasRemoved;
    }

    public List<Student> Search(string text)
    {
        var searchResults = StudentRepository.Search(text);

        return searchResults;
    }

    public void ExportToExcel()
    {
        var students = StudentRepository.GetAll();
        StudentExcelBroker.Export(students);
    }
}
