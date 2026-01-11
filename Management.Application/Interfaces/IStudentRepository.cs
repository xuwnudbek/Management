using System.Collections.Generic;
using Management.Domain.Models;

namespace Management.Application.Interfaces;

public interface IStudentRepository
{
    // CRUD methods
    public void Add(Student student);
    public void AddAll(List<Student> students);
    public Student GetById(string id);
    public List<Student> GetAll();
    public bool Update(Student student);
    public bool Remove(string id);
    public List<Student> Search(string text);
}
