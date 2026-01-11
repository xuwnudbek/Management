using System.Collections.Generic;
using Management.Application.Interfaces;
using Management.Domain.Models;
using OfficeOpenXml;

namespace Management.Infrastructure.Brokers
{
    public class StudentExcelBroker : IStudentExcelBroker
    {
        public void Export(List<Student> students)
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Students");

            // Labels
            sheet.Cells["A1"].LoadFromText("ID");
            sheet.Cells["B1"].LoadFromText("First Name");
            sheet.Cells["C1"].LoadFromText("Last Name");

            // Load all students
            sheet.Cells["A2"].LoadFromCollection<Student>(students);

            // Save all changes
            package.Save();

            // Move to Root Directory
            package.File.MoveTo("Students.xlsx");
        }

        public void Import(string filePath) { }
    }
}
