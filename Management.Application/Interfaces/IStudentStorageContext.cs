using System.Collections.Generic;
using Management.Domain.Models;

namespace Management.Application.Interfaces;

public interface IStudentStorageContext
{
    List<Student> Load();
    void Save(List<Student> students);
}
