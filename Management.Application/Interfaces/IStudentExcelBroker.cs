using System.Collections.Generic;
using System.IO;
using Management.Domain.Models;

namespace Management.Application.Interfaces;

public interface IStudentExcelBroker
{
    public void Export(List<Student> students);
    public void Import(string filePath);
}
